namespace StockFlow.Application.DTOs.Products;

// Represents product data returned to the client.
// This DTO is intentionally separated from the domain entity to:
// - prevent exposing internal fields (e.g., audit fields, internal IDs)
// - maintain a stable API contract independent of database changes
public class ProductResponseDto
{
    // Database-generated unique identifier
    public int Id { get; set; }

    // Business identifier used for product tracking (SKU)
    public string SKU { get; set; } = string.Empty;

    // Product name (used for display and search)
    public string Name { get; set; } = string.Empty;

    // Optional size attribute (e.g., S, M, L, numeric sizes)
    public string? Size { get; set; }

    // Optional color attribute (used for filtering/display)
    public string? Color { get; set; }

    // Current available stock quantity
    public int Quantity { get; set; }

    // Purchase (cost) price per unit
    public decimal PurchasePrice { get; set; }

    // Date when the product was purchased/added to inventory
    // Should be treated as UTC for consistency across systems
    public DateTime PurchaseDate { get; set; }
}