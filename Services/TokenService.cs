using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ViltrapportenApi.Data.SystemModels;
using ViltrapportenApi.Modal;

namespace ViltrapportenApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ViltrapportenSystemContext _context;
        private readonly IStringLocalizer<TokenService> _localizer;

        public TokenService(IOptions<JwtSettings> jwtSettings, TokenValidationParameters tokenValidationParameters, ViltrapportenSystemContext context, IStringLocalizer<TokenService> localizer)
        {
            _jwtSettings = jwtSettings.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
            _localizer = localizer;
        }

        public async Task<Result<TokenResponse>> GenerateTokenAsync(string email, int userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return Result<TokenResponse>.Failure(_localizer["EmailRequired"]);

                if (userId <= 0)
                    return Result<TokenResponse>.Failure(_localizer["InvalidUserId"]);

                if (string.IsNullOrWhiteSpace(_jwtSettings.Key))
                    return Result<TokenResponse>.Failure(_localizer["JWTkeyNotConfigured"]);

                var now = DateTime.UtcNow;
                var jwtId = Guid.NewGuid().ToString();

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti, jwtId),
                    new(JwtRegisteredClaimNames.Email, email),
                    new("userId", userId.ToString()),
                    new(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                    new(JwtRegisteredClaimNames.Sub, userId.ToString())
                };

                var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Key);
                var securityKey = new SymmetricSecurityKey(keyBytes);
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = now.Add(_jwtSettings.TokenLifeTime),
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                    SigningCredentials = credentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(securityToken);

                var refreshToken = new RefreshToken
                {
                    Token = GenerateSecureGuid(),
                    JwtId = jwtId,
                    UserId = userId,
                    CreationDate = now,
                    ExpiryDate = now.AddDays(_jwtSettings.RefreshTokenExpirationDays)
                };

                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();

                var response = new TokenResponse
                {
                    Token = jwt,
                    RefreshToken = refreshToken.Token
                };

                return Result<TokenResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<TokenResponse>.Failure($"{_localizer["TokenGenerationFailed"]}: {ex.Message}");
            }
        }

        public async Task<Result<TokenResponse>> RefreshTokenAsync(string token, Guid refreshToken)
        {
         await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var principal = GetPrincipalFromToken(token);
                if (principal == null)
                    return Result<TokenResponse>.Failure(_localizer["InvalidToken"]);

                if (!IsTokenExpired(principal))
                {
                    return Result<TokenResponse>.Success(new TokenResponse
                    {
                        Token = token,
                        RefreshToken = refreshToken
                    }, _localizer["TokenNotExpiredYet"]);
                }

                var storedRefresh = await GetValidRefreshTokenAsync(refreshToken, principal);
                if (!storedRefresh.IsSuccess)
                    return Result<TokenResponse>.Failure(storedRefresh.Message);

                var (email, userId) = ExtractUserInfo(principal);
                if (string.IsNullOrEmpty(email) || userId == null)
                    return Result<TokenResponse>.Failure(_localizer["InvalidTokenData"]);

                var result = await GenerateTokenAsync(email, userId.Value);
                if (!result.IsSuccess)
                    return Result<TokenResponse>.Failure(_localizer["TokenGenerationFailed"]);

                await RevokeRefreshTokenAsync(storedRefresh.Value);
                await transaction.CommitAsync();

                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<TokenResponse>.Failure(_localizer["TokenRefreshFailed"]);
            }
        }

        private static Guid GenerateSecureGuid()
        {
            var randomBytes = new byte[16];
            RandomNumberGenerator.Fill(randomBytes);
            return new Guid(randomBytes);
        }

        private bool IsTokenExpired(ClaimsPrincipal principal)
        {
            var exp = principal.FindFirstValue(JwtRegisteredClaimNames.Exp);
            if (!long.TryParse(exp, out var expUnix))
                return true;

            var expiryDate = DateTime.UnixEpoch.AddSeconds(expUnix);
            return expiryDate <= DateTime.UtcNow;
        }

        private (string? email, int? userId) ExtractUserInfo(ClaimsPrincipal principal)
        {
            var email = principal.FindFirstValue(JwtRegisteredClaimNames.Email);
            var userIdClaim = principal.FindFirstValue("userId");

            if (int.TryParse(userIdClaim, out int userId))
                return (email, userId);

            return (email, null);
        }

        private async Task<Result<RefreshToken>> GetValidRefreshTokenAsync(Guid token, ClaimsPrincipal principal)
        {
            var jti = principal.FindFirstValue(JwtRegisteredClaimNames.Jti);
            if (string.IsNullOrEmpty(jti))
                return Result<RefreshToken>.Failure(_localizer["InvalidTokenId"]);

            var stored = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token);
            if (stored == null)
                return Result<RefreshToken>.Failure(_localizer["RefreshTokenNotFound"]);

            if (stored.Used || stored.Invalidated || stored.ExpiryDate <= DateTime.UtcNow || stored.JwtId != jti)
                return Result<RefreshToken>.Failure(_localizer["InvalidRefreshToken"]);

            return Result<RefreshToken>.Success(stored);
        }

        private async Task RevokeRefreshTokenAsync(RefreshToken oldToken)
        {
            oldToken.Used = true;
            _context.RefreshTokens.Update(oldToken);
            await _context.SaveChangesAsync();
        }

        private ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var parameters = _tokenValidationParameters.Clone();
                parameters.ValidateLifetime = false;

                var principal = tokenHandler.ValidateToken(token, parameters, out var validatedToken);
                return IsJwtWithValidSecurityAlgorithm(validatedToken) ? principal : null;
            }
            catch
            {
                return null;
            }
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<Result<bool>> InvalidateRefreshTokenAsync(Guid refreshToken)
        {
            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);
            if (storedRefreshToken == null)
                return Result<bool>.Failure(_localizer["InvalidRefreshToken"]);

            storedRefreshToken.Invalidated = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}


//public async Task<Result<TokenResponse>> RefreshTokenAsync1(string token, Guid refreshToken)
//{
//    try
//    {
//        var validatedToken = GetPrincipalFromToken(token);
//        if (validatedToken == null)
//            return Result<TokenResponse>.Failure(_localizer["InvalidToken"]);

//        // Retrieve the token expiry date from the JWT claims
//        var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
//        var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

//        if (expiryDateTimeUtc > DateTime.UtcNow)
//        {
//            return Result<TokenResponse>.Success(new TokenResponse
//            {
//                Token = token,
//                RefreshToken = refreshToken,
//                Error = _localizer["TokenNotExpiredYet"]
//            });
//        }

//        var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
//        var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);
//        if (storedRefreshToken == null || storedRefreshToken.JwtId != jti || storedRefreshToken.Invalidated)
//            return Result<TokenResponse>.Failure(_localizer["InvalidRefreshToken"]);

//        if (storedRefreshToken.ExpiryDate < DateTime.UtcNow)
//            return Result<TokenResponse>.Failure(_localizer["ExpiredRefreshToken"]);

//        storedRefreshToken.Used = true;
//        _context.RefreshTokens.Update(storedRefreshToken);
//        await _context.SaveChangesAsync();

//        var email = validatedToken.Claims.Single(x => x.Type == ClaimTypes.Email).Value;
//        int.TryParse(validatedToken.Claims.Single(x => x.Type == "userId").Value, out int userId);
//        var role = validatedToken.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value ?? "defaultRole";

//        return await GenerateTokenAsync(email, userId);
//    }
//    catch (Exception ex)
//    {
//        return Result<TokenResponse>.Failure($"{_localizer["TokenRefreshFailed"]}\n {ex.Message}");
//    }
//}