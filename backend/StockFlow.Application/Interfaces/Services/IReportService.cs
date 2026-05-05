using StockFlow.Application.DTOs.Reports;

namespace StockFlow.Application.Interfaces.Services;

// Defines reporting and analytics operations for inventory.
// Responsible for both data-level reports (JSON) and document-level reports (PDF).
// Acts as the boundary between controllers and reporting logic.
public interface IReportService
{
    // Returns aggregated inventory metrics (KPIs) such as totals and averages.
    Task<InventorySummaryDto> GetInventorySummaryAsync(int threshold);

    // Returns full inventory data for reporting (all products).
    Task<List<InventoryReportItemDto>> GetFullInventoryReportAsync();

    // Returns products with stock below or equal to the specified threshold.
    Task<List<InventoryReportItemDto>> GetLowStockReportAsync(int threshold);

    // Returns products filtered by purchase date range.
    // Date validation and business rules should be handled in the service implementation.
    Task<List<InventoryReportItemDto>> GetDateRangeReportAsync(DateTime from, DateTime to);

    // Generates a PDF document for the full inventory report.
    // Combines detailed data with summary metrics and formatted layout.
    Task<byte[]> GenerateFullInventoryPdfAsync();

    // Generates a PDF document for low-stock products.
    Task<byte[]> GenerateLowStockPdfAsync(int threshold);
}