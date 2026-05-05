namespace StockFlow.Application.DTOs.Products;

// Represents input data required to create a new product record.
// Validation rules (e.g., required fields, non-negative values, uniqueness)
// should be enforced via FluentValidation and service layer.
public class CreateProductDto
{
    // Stock Keeping Unit (SKU) - unique identifier for a product.
    // Must be unique across the system (enforced at DB + service level).
    public string SKU { get; set; } = string.Empty;

    // Product name (required for identification and search).
    public string Name { get; set; } = string.Empty;

    // Optional size attribute (e.g., S, M, L, XL, numeric sizes).
    public string? Size { get; set; }

    // Optional color attribute (used for filtering and display).
    public string? Color { get; set; }

    // Available stock quantity.
    // Must be >= 0 (validated before persistence).
    public int Quantity { get; set; }

    // Purchase price per unit.
    // Must be >= 0 and represents cost price (not selling price).
    public decimal PurchasePrice { get; set; }

    // Date when the product was purchased or stocked.
    // Should typically be in UTC for consistency.
    public DateTime PurchaseDate { get; set; }
}