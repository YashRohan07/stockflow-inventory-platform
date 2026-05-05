using StockFlow.Application.DTOs.Reports;

namespace StockFlow.Application.Interfaces.Services;

// Defines contract for generating PDF reports.
// This is an application-level abstraction for report rendering,
// while the actual PDF library implementation resides in Infrastructure.
public interface IPdfReportGenerator
{
    // Generates a PDF document for the full inventory report.
    // Combines detailed item data with summary metrics into a formatted document.
    // Returns the generated PDF as a byte array for download/streaming.
    byte[] GenerateInventoryReportPdf(
        string title,
        List<InventoryReportItemDto> items,
        InventorySummaryDto summary);
}