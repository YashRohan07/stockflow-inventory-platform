namespace StockFlow.Application.DTOs.Reports;

// Represents aggregated inventory metrics for dashboard and reporting purposes.
// All values are derived from underlying product data and computed in the service layer.
public class InventorySummaryDto
{
    // Total number of distinct products in the system
    public int TotalProducts { get; set; }

    // Sum of all product quantities (total stock across inventory)
    public int TotalQuantity { get; set; }

    // Average purchase price per product
    // Note: Calculation method (simple vs weighted average) should be defined in service logic
    public decimal AveragePrice { get; set; }

    // Total inventory value = sum of (Quantity * PurchasePrice) across all products
    public decimal TotalInventoryValue { get; set; }

    // Number of products considered low stock based on a defined threshold
    public int LowStockProducts { get; set; }
}