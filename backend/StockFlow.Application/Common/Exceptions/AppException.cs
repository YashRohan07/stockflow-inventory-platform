namespace StockFlow.Application.Common.Exceptions;

// Base exception type for controlled application errors.
// Used to throw predictable, business-level exceptions from the Application layer
// which are later translated into HTTP responses by ExceptionMiddleware.
public class AppException : Exception
{
    // HTTP status code associated with this exception (e.g., 400, 404, 409).
    // Allows the middleware to map application errors to proper API responses.
    public int StatusCode { get; }

    // Initializes a new instance of AppException with a message and optional status code.
    // Default status code is 400 (Bad Request) for validation or business rule failures.
    public AppException(string message, int statusCode = 400)
        : base(message) // Pass message to base Exception class
    {
        StatusCode = statusCode;
    }
}