using Microsoft.Identity.Client;

namespace ViltrapportenApi.Errors
{
    public class ApiException(int statusCode, string? message = null, string? details = null) : ApiResponse<string>(statusCode, details, message)
    {
        public string Details { get; set; } = details;
    }
}
