namespace StockFlow.Application.Common.Models;

// This class defines a standard format for all API responses
// It ensures every response (success or error) looks consistent
public class ApiResponse<T>
{
    // Indicates whether the request was successful
    // true = success, false = failure
    public bool Success { get; set; }

    // A message describing the result
    // This is required, so it must always be provided
    // Example: "Data fetched successfully" or "Product not found"
    public required string Message { get; set; }

    // The actual data returned from the API
    // Generic type (T) allows flexibility (object, list, etc.)
    // Can be null when there is no data (e.g., error case)
    public T? Data { get; set; }

    // Contains error details (if any)
    // Used for validation errors or additional error info
    // Will be null when there are no errors
    public object? Errors { get; set; }
}