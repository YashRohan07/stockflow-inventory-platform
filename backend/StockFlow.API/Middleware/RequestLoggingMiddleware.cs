using System.Diagnostics;

namespace StockFlow.API.Middleware;

// Middleware for logging incoming requests and outgoing responses.
// Helps in tracing request flow, debugging, and basic performance monitoring.
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // Logs request start, execution time, and final response status.
    // Works alongside ExceptionMiddleware to provide full request lifecycle visibility.
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // Log incoming request details (method + path)
        _logger.LogInformation(
            "Incoming Request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);

        try
        {
            // Pass control to the next middleware in the pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log exception at request level (ExceptionMiddleware will handle response formatting)
            _logger.LogError(ex, "Error occurred while processing request");
            throw;
        }

        stopwatch.Stop();

        // Log response details with execution time (basic performance insight)
        _logger.LogInformation(
            "Completed Request: {Method} {Path} → {StatusCode} in {ElapsedMilliseconds}ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}