using ViltrapportenApi.Modal;

namespace ViltrapportenApi.Services
{
    public interface ITokenService
    {
        Task<Result<TokenResponse>> GenerateTokenAsync(string email, int userId);

        Task<Result<TokenResponse>> RefreshTokenAsync(string token, Guid refreshToken);
        Task<Result<bool>> InvalidateRefreshTokenAsync(Guid refreshToken);
    }
}
