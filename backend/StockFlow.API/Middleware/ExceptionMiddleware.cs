// Used for HTTP status codes like 500, 400
using System.Net;

// Used to convert C# object to JSON
using System.Text.Json;

// Import custom exception class
using StockFlow.Application.Common.Exceptions;

// Import standard API response model
using StockFlow.Application.Common.Models;

namespace StockFlow.API.Middleware;

// This middleware catches all unhandled errors in the application
public class ExceptionMiddleware
{
    // This represents the next step in the request pipeline
    private readonly RequestDelegate _next;

    // Constructor: receives the next middleware
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // This method runs for every incoming request
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Pass request to the next middleware or controller
            await _next(context);
        }
        catch (Exception ex)
        {
            // If any error happens, handle it here
            await HandleExceptionAsync(context, ex);
        }
    }

    // This method creates a standard error response
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Create a standard API response object
        var response = new ApiResponse<object>
        {
            Success = false,              // request failed
            Message = exception.Message,  // error message
            Data = null                  // no data in error case
        };

        // Set response type to JSON
        context.Response.ContentType = "application/json";

        // Check if it's a custom AppException
        if (exception is AppException appEx)
        {
            // Use custom status code (like 400, 404)
            context.Response.StatusCode = appEx.StatusCode;
        }
        else
        {
            // For unknown errors, return 500 Internal Server Error
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        // Convert response object to JSON string
        var json = JsonSerializer.Serialize(response);

        // Send JSON response back to client
        return context.Response.WriteAsync(json);
    }
}