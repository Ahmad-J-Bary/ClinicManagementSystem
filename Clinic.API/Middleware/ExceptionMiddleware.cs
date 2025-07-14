using Clinic.Application.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Clinic.API.Middleware
{
    /// <summary>
    /// Middleware for centralized exception handling.
    /// Catches all unhandled exceptions and returns appropriate HTTP responses.
    /// Inspired by HR.LeaveManagement.Clean exception handling pattern.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ErrorResponse
            {
                Success = false,
                Message = exception.Message
            };

            switch (exception)
            {
                case ValidationException validationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = "Validation failed";
                    errorResponse.Errors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                    _logger.LogWarning("Validation exception: {Message}", validationException.Message);
                    break;

                case BadRequestException badRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = badRequestException.Message;
                    _logger.LogWarning("Bad request exception: {Message}", badRequestException.Message);
                    break;

                case NotFoundException notFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = notFoundException.Message;
                    _logger.LogWarning("Not found exception: {Message}", notFoundException.Message);
                    break;

                case UnauthorizedAccessException unauthorizedException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.Message = "Unauthorized access";
                    _logger.LogWarning("Unauthorized access exception: {Message}", unauthorizedException.Message);
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "An internal server error occurred";
                    _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }

    /// <summary>
    /// Standard error response model for API exceptions.
    /// </summary>
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
    }
}

