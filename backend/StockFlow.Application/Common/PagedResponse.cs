namespace StockFlow.Application.Common;

// Generic paged response for list APIs.
// This helps frontend show pagination information properly.
public class PagedResponse<T>
{
    // Current page items.
    public List<T> Items { get; set; } = new();

    // Current page number.
    public int Page { get; set; }

    // Number of items per page.
    public int PageSize { get; set; }

    // Total items before pagination.
    public int TotalCount { get; set; }

    // Total pages calculated from total count and page size.
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    // Useful for frontend pagination buttons.
    public bool HasPreviousPage => Page > 1;

    // Useful for frontend pagination buttons.
    public bool HasNextPage => Page < TotalPages;
}