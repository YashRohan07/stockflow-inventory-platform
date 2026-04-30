namespace StockFlow.Application.DTOs.Products;

// This DTO receives query parameters from the frontend/API client.
// Example:
// /api/products?search=shirt&purchaseDateFrom=2026-01-01&sortBy=price&sortOrder=asc&page=1&pageSize=10
public class ProductQueryParametersDto
{
    // Search text for SKU or product name.
    public string? Search { get; set; }

    // Optional purchase date start filter.
    public DateTime? PurchaseDateFrom { get; set; }

    // Optional purchase date end filter.
    public DateTime? PurchaseDateTo { get; set; }

    // Sorting field.
    // Allowed values will be handled in repository:
    // purchaseDate, price, quantity, name
    public string SortBy { get; set; } = "purchaseDate";

    // Sorting direction.
    // Allowed values: asc, desc
    public string SortOrder { get; set; } = "desc";

    private int _page = 1;

    // Page number should never be less than 1.
    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    private int _pageSize = 10;

    // Page size is limited to avoid heavy database queries.
    // Simple production-style protection.
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 10 : value > 50 ? 50 : value;
    }
}