namespace StockFlow.Application.DTOs.Products;

// Represents input data for updating an existing product.
// SKU is intentionally excluded as it is treated as an immutable business identifier.
public class UpdateProductDto
{
    // Product name (required for identification and search)
    public string Name { get; set; } = string.Empty;

    // Optional size attribute (e.g., S, M, L, numeric sizes)
    public string? Size { get; set; }

    // Optional color attribute (used for filtering/display)
    public string? Color { get; set; }

    // Available stock quantity
    // Must be >= 0 (validated before persistence)
    public int Quantity { get; set; }

    // Purchase (cost) price per unit
    // Must be >= 0 and represents cost price (not selling price)
    public decimal PurchasePrice { get; set; }

    // Date when the product was purchased or stocked
    // Should be handled as UTC for consistency
    public DateTime PurchaseDate { get; set; }
}