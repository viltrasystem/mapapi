
using LoginValidatorServiceReference;
using Microsoft.Extensions.Localization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Description;
using ViltrapportenApi.Data.SystemModels;
using ViltrapportenApi.Modal;

namespace ViltrapportenApi.Services
{
    public class WcfAuthenticationService : IWcfAuthenticationService
    {
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        private readonly string? _serviceEndpoint;
        private readonly ITokenService _tokenService;
        private readonly IStringLocalizer<TokenService> _localizer;

        public WcfAuthenticationService(IConfiguration configuration, IDataService dataService, ITokenService tokenService, IStringLocalizer<TokenService> localizer)
        {

            _dataService = dataService;
            _configuration = configuration;
            _tokenService = tokenService;

            _serviceEndpoint = _configuration["ServiceSettings:LoginValidatorEndpoint"];
            if (string.IsNullOrEmpty(_serviceEndpoint) || !Uri.IsWellFormedUriString(_serviceEndpoint, UriKind.Absolute))
            {
                throw new InvalidOperationException("LoginValidatorEndpoint is not properly configured.");
            }
        }

        public async Task<AuthResponseDto> WcfAuthenticateAsync(string username, string password, string ipAddress)
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(_serviceEndpoint);
            var client = new LoginValidatorClient(binding, endpoint);

            try
            {
                bool isAuthenticated = await client.ValidateUserAsync(username, password, ipAddress);
                if (!isAuthenticated)
                {
                    return AuthResponseDto.Failure("InvalidCredentials", _localizer["LoginFailed"]);
                }

                var user = await client.GetDnnUserIdAsync(username);
                var tokenResult = await _tokenService.GenerateTokenAsync(user.Email, (int)user.UserDnnId);

                if (!tokenResult.IsSuccess)
                {
                    return AuthResponseDto.Failure("TokenGenerationFailed", tokenResult.Message);
                }

                return AuthResponseDto.Success(user.UserDnnId, user.IsAdmin, user.DisplayName, tokenResult.Value.Token, tokenResult.Value.RefreshToken);
            }
            catch (CommunicationException ex)
            {
                throw new ApplicationException("A communication error occurred while authenticating the user.", ex);
            }
            catch (TimeoutException ex)
            {
                throw new ApplicationException("The request timed out while communicating with the WCF service.", ex);
            }
            finally
            {
                if (client.State == CommunicationState.Faulted)
                {
                    client.Abort();
                }
                else
                {
                    await client.CloseAsync();
                }
            }
        }
    }
}
