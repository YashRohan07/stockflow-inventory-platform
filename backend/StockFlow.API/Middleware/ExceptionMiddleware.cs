using System.Text.Json;
using StockFlow.Application.Common.Exceptions;
using StockFlow.Application.Common.Models;

namespace StockFlow.API.Middleware;

// Centralized middleware for converting application exceptions into consistent API responses.
// This keeps controllers and services clean by avoiding repeated try-catch blocks.
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // Executes the next middleware in the pipeline and catches exceptions globally.
    // Any unhandled exception is logged and returned using the standard ApiResponse format.
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log full exception details internally while returning a safe message to the client.
            _logger.LogError(ex, "Unhandled exception occurred.");

            await HandleExceptionAsync(context, ex);
        }
    }

    // Maps known exception types to appropriate HTTP status codes.
    // Unknown exceptions are hidden behind a generic 500 response to avoid leaking internal details.
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        int statusCode;
        string message;

        switch (exception)
        {
            case AppException appEx:
                statusCode = appEx.StatusCode;
                message = appEx.Message;
                break;

            case UnauthorizedException:
                statusCode = StatusCodes.Status401Unauthorized;
                message = exception.Message;
                break;

            case KeyNotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                message = exception.Message;
                break;

            case ArgumentException:
                statusCode = StatusCodes.Status400BadRequest;
                message = exception.Message;
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = "Something went wrong. Please try again later.";
                break;
        }

        context.Response.StatusCode = statusCode;

        var response = new ApiResponse<object>
        {
            Success = false,
            Message = message,
            Errors = null
        };

        var json = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(json);
    }
}