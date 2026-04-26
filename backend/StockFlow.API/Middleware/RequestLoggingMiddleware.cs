using System.Diagnostics;

namespace StockFlow.API.Middleware;

// This middleware logs basic information about every HTTP request
// It helps track what request came and how long it took to process
public class RequestLoggingMiddleware
{
    // Represents the next middleware in the pipeline
    private readonly RequestDelegate _next;

    // Logger used to write logs to console/file
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    // Constructor: receives next middleware and logger from DI
    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // This method runs for every incoming request
    public async Task InvokeAsync(HttpContext context)
    {
        // Start a timer to measure how long the request takes
        var stopwatch = Stopwatch.StartNew();

        // Log incoming request details (method + URL path)
        _logger.LogInformation(
            "Incoming Request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);

        // Pass the request to the next middleware or controller
        await _next(context);

        // Stop the timer after request processing is done
        stopwatch.Stop();

        // Log completed request with:
        // HTTP method, path, status code, and time taken
        _logger.LogInformation(
            "Completed Request: {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}