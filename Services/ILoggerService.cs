namespace ViltrapportenApi.Services
{
    public interface ILoggerService
    {
        Task LogAsync(string message, LogLevel level = LogLevel.Information, object? data = null);
    }
}
