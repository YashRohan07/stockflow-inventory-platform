namespace StockFlow.Application.Common.Exceptions;

// Represents authentication or authorization failures.
// Used when a user is not authenticated or does not have permission to access a resource.
// Handled by ExceptionMiddleware to return HTTP 401 Unauthorized.
public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message)
        : base(message)
    {
    }
}