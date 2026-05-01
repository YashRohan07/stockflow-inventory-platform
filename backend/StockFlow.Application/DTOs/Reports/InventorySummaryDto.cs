namespace StockFlow.Application.DTOs.Reports;

// Represents summarized inventory information.
public class InventorySummaryDto
{
    public int TotalProducts { get; set; }

    public int TotalQuantity { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal TotalInventoryValue { get; set; }

    public int LowStockProducts { get; set; }
}