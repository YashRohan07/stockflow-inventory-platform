namespace StockFlow.Application.Common;

// Standard paginated response wrapper for list-based APIs.
// Provides both data and metadata required for frontend pagination handling.
public class PagedResponse<T>
{
    // Items for the current page
    public List<T> Items { get; set; } = new();

    // Current page number (1-based indexing)
    public int Page { get; set; }

    // Number of items per page
    public int PageSize { get; set; }

    // Total number of items before pagination
    public int TotalCount { get; set; }

    // Total number of pages based on TotalCount and PageSize
    // Note: Assumes PageSize > 0 (should be validated at request level)
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    // Indicates if there is a previous page available
    public bool HasPreviousPage => Page > 1;

    // Indicates if there is a next page available
    public bool HasNextPage => Page < TotalPages;
}