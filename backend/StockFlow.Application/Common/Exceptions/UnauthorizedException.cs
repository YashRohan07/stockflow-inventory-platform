namespace StockFlow.Application.Common.Exceptions;

// Custom exception for authentication failure
public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message)
        : base(message)
    {
    }
}