namespace StockFlow.Application.DTOs.Reports;

// Represents one product row inside an inventory report.
public class InventoryReportItemDto
{
    public string SKU { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Size { get; set; }

    public string? Color { get; set; }

    public int Quantity { get; set; }

    public decimal PurchasePrice { get; set; }

    public DateTime PurchaseDate { get; set; }

    // Calculated value: quantity * purchase price.
    public decimal TotalValue { get; set; }
}