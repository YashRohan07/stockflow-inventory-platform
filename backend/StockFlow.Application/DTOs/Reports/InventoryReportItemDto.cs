namespace StockFlow.Application.DTOs.Reports;

// Represents a single product entry within an inventory report.
// This DTO is optimized for reporting use-cases and may include derived fields.
public class InventoryReportItemDto
{
    // Business identifier for the product
    public string SKU { get; set; } = string.Empty;

    // Product name for display in reports
    public string Name { get; set; } = string.Empty;

    // Optional size attribute
    public string? Size { get; set; }

    // Optional color attribute
    public string? Color { get; set; }

    // Current available quantity
    public int Quantity { get; set; }

    // Purchase (cost) price per unit
    public decimal PurchasePrice { get; set; }

    // Date when the product was purchased
    // Should be treated as UTC for consistency
    public DateTime PurchaseDate { get; set; }

    // Total inventory value for this item (Quantity * PurchasePrice)
    // This is a derived/calculated field, typically computed in the service layer.
    public decimal TotalValue { get; set; }
}