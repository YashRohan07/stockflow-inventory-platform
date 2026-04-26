namespace StockFlow.Application.Common.Exceptions;

// Custom exception class for handling application errors
// We use this to throw controlled errors with a status code (like 400, 404)
public class AppException : Exception
{
    // This stores the HTTP status code (e.g., 400, 404, 500)
    public int StatusCode { get; }

    // Constructor: takes error message and optional status code
    public AppException(string message, int statusCode = 400)
        : base(message) // pass message to base Exception class
    {
        // Set the status code
        StatusCode = statusCode;
    }
}