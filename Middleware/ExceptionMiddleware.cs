using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ViltrapportenApi.Errors;
using ViltrapportenApi.Services;

namespace ViltrapportenApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILoggerService loggerService, IHostEnvironment env)
        {
            _next = next;
            _loggerService = loggerService;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await _loggerService.LogAsync($"An unexpected error occurred: {ex.Message}", LogLevel.Error);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;

            // Customize status code and message based on exception type
            switch (ex)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case ValidationException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case DbUpdateException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = ex.GetType().Name,
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            if (_env.IsDevelopment())
            {
                problemDetails.Extensions.Add("stackTrace", ex.StackTrace);
            }

            var response = new ApiResponse<ProblemDetails>(
                (int)statusCode,
                problemDetails,
                ex.Message
            );

            await _loggerService.LogAsync(
                $"An error occurred: {ex.Message}",
                statusCode == HttpStatusCode.InternalServerError ? LogLevel.Error : LogLevel.Warning,
                new
                {
                    StatusCode = (int)statusCode,
                    ExceptionType = ex.GetType().Name,
                    RequestPath = context.Request.Path,
                    RequestMethod = context.Request.Method,
                    StackTrace = _env.IsDevelopment() ? ex.StackTrace : null
                }
            );

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = _env.IsDevelopment() // Pretty-print JSON in development
            });

            await context.Response.WriteAsync(json);
        }
    }

    // Custom exception for when a resource is not found
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    // Custom exception for validation errors
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }

    public class ProblemDetails
    {
        public int Status { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public string? Instance { get; set; }
        public IDictionary<string, object?> Extensions { get; set; } = new Dictionary<string, object?>();
    }
}

//public class ExceptionMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger<ExceptionMiddleware> _logger;
//    private readonly IHostEnvironment _env;

//    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
//    {
//        _next = next;
//        _logger = logger;
//        _env = env;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        try
//        {
//            await _next(context);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
//            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again later.", ex);
//        }
//    }

//    private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string customMessage, Exception ex)
//    {
//        context.Response.ContentType = "application/json";
//        context.Response.StatusCode = (int)statusCode;

//        var response = new ApiException((int)statusCode, customMessage, _env.IsDevelopment() ? $"{ex.Message}\n{ex.StackTrace}" : $"{ex.Message}\n{ex.StackTrace}");

//        _logger.Log(
//            statusCode == HttpStatusCode.InternalServerError ? LogLevel.Error : LogLevel.Warning,
//            ex,
//            "{Message}",
//            customMessage
//        );

//        var options = new JsonSerializerOptions
//        {
//            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//        };
//        var json = JsonSerializer.Serialize(response, options);

//        await context.Response.WriteAsync(json);
//    }
//}
