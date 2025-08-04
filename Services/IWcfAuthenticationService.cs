using ViltrapportenApi.Modal;

namespace ViltrapportenApi.Services
{
    public interface IWcfAuthenticationService
    {
        Task<AuthResponseDto> WcfAuthenticateAsync(string username, string password, string ipAddress);
    }
}
