namespace StockFlow.Application.DTOs.Products;

// Represents query parameters for retrieving product lists.
// Supports searching, filtering, sorting, and pagination.
// Acts as a contract between API and data access layer.
public class ProductQueryParametersDto
{
    // Search term applied to SKU or product name.
    public string? Search { get; set; }

    // Filter: include products purchased on or after this date
    public DateTime? PurchaseDateFrom { get; set; }

    // Filter: include products purchased on or before this date
    public DateTime? PurchaseDateTo { get; set; }

    // Field used for sorting results.
    // Expected values: purchaseDate, price, quantity, name
    // Note: Validation/whitelisting should be enforced in repository/service layer.
    public string SortBy { get; set; } = "purchaseDate";

    // Sorting direction: asc (ascending) or desc (descending)
    // Note: Invalid values should be normalized in service/repository layer.
    public string SortOrder { get; set; } = "desc";

    private int _page = 1;

    // Page number (1-based indexing).
    // Automatically corrected to minimum value of 1.
    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    private int _pageSize = 10;

    // Number of items per page.
    // Constrained to prevent excessive data load (basic performance protection).
    // Range: 1 to 50
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 10 : value > 50 ? 50 : value;
    }
}