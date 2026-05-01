using StockFlow.Application.DTOs.Reports;

namespace StockFlow.Application.Interfaces.Services;

public interface IReportService
{
    Task<InventorySummaryDto> GetInventorySummaryAsync(int threshold);

    Task<List<InventoryReportItemDto>> GetFullInventoryReportAsync();

    Task<List<InventoryReportItemDto>> GetLowStockReportAsync(int threshold);

    Task<List<InventoryReportItemDto>> GetDateRangeReportAsync(DateTime from, DateTime to);

    Task<byte[]> GenerateFullInventoryPdfAsync();

    Task<byte[]> GenerateLowStockPdfAsync(int threshold);
}