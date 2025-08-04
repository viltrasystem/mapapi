using System.Text.Json;
using ViltrapportenApi.Services;

namespace ViltrapportenApi.Modal
{
    public class LoggerService : ILoggerService
    {
        private readonly string _logFilePath;
        private readonly SemaphoreSlim _semaphore = new(1, 1); // For thread-safe file access
        private readonly long _maxLogFileSize = 10 * 1024 * 1024; // 10 MB max log file size

        public LoggerService(IConfiguration configuration)
        {
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDirectory);
            _logFilePath = Path.Combine(logDirectory, "application.log");
        }

        public async Task LogAsync(string message, LogLevel level = LogLevel.Information, object? data = null)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

            if (data != null)
            {
                logEntry += $" | Data: {JsonSerializer.Serialize(data)}";
            }

            await _semaphore.WaitAsync();
            try
            {
                if (File.Exists(_logFilePath) && new FileInfo(_logFilePath).Length > _maxLogFileSize)
                {
                    RotateLogFile();
                }

                await File.AppendAllTextAsync(_logFilePath, logEntry + Environment.NewLine);
            }
            finally
            {
                _semaphore.Release();
            }

            Console.WriteLine(logEntry);
        }

        private void RotateLogFile()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var newLogFileName = $"application_{timestamp}.log";
            var newLogFilePath = Path.Combine(Path.GetDirectoryName(_logFilePath)!, newLogFileName);

            File.Move(_logFilePath, newLogFilePath);
        }
    }
}