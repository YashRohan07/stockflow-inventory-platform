using StockFlow.Application.DTOs.Reports;

namespace StockFlow.Application.Interfaces.Services;

// Abstraction for PDF generation.
// Application layer knows the contract, Infrastructure provides implementation.
public interface IPdfReportGenerator
{
    byte[] GenerateInventoryReportPdf(
        string title,
        List<InventoryReportItemDto> items,
        InventorySummaryDto summary);
}