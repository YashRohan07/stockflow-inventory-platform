namespace StockFlow.Application.Common.Models;

// Standard API response wrapper used across all endpoints.
// Ensures consistent response structure for both success and error cases.
public class ApiResponse<T>
{
    // Indicates whether the request was successful
    public bool Success { get; set; }

    // Human-readable message describing the result
    public required string Message { get; set; }

    // Actual response data (if any)
    public T? Data { get; set; }

    // Additional error details (used in validation or failure scenarios)
    public object? Errors { get; set; }

    // Creates a standardized success response.
    // Intended to be used in controllers instead of manual response construction.
    public static ApiResponse<T> SuccessResponse(T data, string message)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    // Creates a standardized failure response.
    // Used by middleware or services to return structured error details.
    public static ApiResponse<T> FailResponse(string message, object? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }
}