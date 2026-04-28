using System.Net;
using System.Text.Json;
using StockFlow.Application.Common.Exceptions;

namespace StockFlow.API.Middleware;

// This middleware catches all unhandled exceptions globally
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Pass request to next middleware
            await _next(context);
        }
        catch (Exception ex)
        {
            // If any exception occurs → handle it here
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Default response type
        context.Response.ContentType = "application/json";

        // 🔹 Decide status code based on exception type
        var statusCode = exception switch
        {
            UnauthorizedException => StatusCodes.Status401Unauthorized, // Login Issues
            ApplicationException => StatusCodes.Status400BadRequest,   // Business validation
            _ => StatusCodes.Status500InternalServerError              // Unknown error
        };

        context.Response.StatusCode = statusCode;

        // 🔹 Structured error response (clean API response)
        var response = new
        {
            statusCode = statusCode,
            message = exception.Message
        };

        var jsonResponse = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }
}