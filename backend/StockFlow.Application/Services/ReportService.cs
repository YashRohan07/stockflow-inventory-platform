using StockFlow.Application.DTOs.Reports;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Application.Interfaces.Services;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Services;

// Handles inventory reporting and analytics logic.
// Coordinates product report data, summary metrics, low-stock rules, and PDF generation.
public class ReportService : IReportService
{
    private const int DefaultLowStockThreshold = 20;

    private readonly IProductRepository _productRepository;
    private readonly IPdfReportGenerator _pdfReportGenerator;

    public ReportService(
        IProductRepository productRepository,
        IPdfReportGenerator pdfReportGenerator)
    {
        // Repository provides inventory data and aggregate metrics.
        _productRepository = productRepository;

        // PDF generator handles document rendering outside the service business flow.
        _pdfReportGenerator = pdfReportGenerator;
    }

    // Builds high-level inventory summary metrics for dashboard/reporting use.
    // Invalid thresholds are normalized to the default threshold for summary requests.
    public async Task<InventorySummaryDto> GetInventorySummaryAsync(int threshold)
    {
        if (threshold <= 0)
        {
            threshold = DefaultLowStockThreshold;
        }

        return new InventorySummaryDto
        {
            TotalProducts = await _productRepository.GetTotalProductsCountAsync(),
            TotalQuantity = await _productRepository.GetTotalQuantityAsync(),
            AveragePrice = Math.Round(await _productRepository.GetAveragePriceAsync(), 2),
            TotalInventoryValue = await _productRepository.GetTotalInventoryValueAsync(),
            LowStockProducts = await _productRepository.GetLowStockCountAsync(threshold)
        };
    }

    // Returns all inventory items formatted for report output.
    public async Task<List<InventoryReportItemDto>> GetFullInventoryReportAsync()
    {
        var products = await _productRepository.GetAllForReportAsync();

        return products
            .Select(MapToReportItemDto)
            .ToList();
    }

    // Returns low-stock inventory items based on the provided threshold.
    // Stored procedure is used here as a database-side reporting optimization/demo.
    public async Task<List<InventoryReportItemDto>> GetLowStockReportAsync(int threshold)
    {
        ValidateThreshold(threshold);

        var products = await _productRepository.GetLowStockUsingStoredProcedureAsync(threshold);

        return products
            .Select(MapToReportItemDto)
            .ToList();
    }

    // Returns inventory items purchased within a selected date range.
    // Date validation is handled here so controllers remain thin.
    public async Task<List<InventoryReportItemDto>> GetDateRangeReportAsync(DateTime from, DateTime to)
    {
        if (from > to)
        {
            throw new ArgumentException("From date cannot be greater than To date.");
        }

        var products = await _productRepository.GetByPurchaseDateRangeAsync(from, to);

        return products
            .Select(MapToReportItemDto)
            .ToList();
    }

    // Generates a downloadable PDF document for the full inventory report.
    // Combines detailed report rows with summary metrics.
    public async Task<byte[]> GenerateFullInventoryPdfAsync()
    {
        var products = await _productRepository.GetAllForReportAsync();

        var items = products
            .Select(MapToReportItemDto)
            .ToList();

        var summary = await GetInventorySummaryAsync(DefaultLowStockThreshold);

        return _pdfReportGenerator.GenerateInventoryReportPdf(
            "Full Inventory Report",
            items,
            summary);
    }

    // Generates a downloadable PDF document for low-stock products.
    // Summary is calculated from the filtered low-stock product set.
    public async Task<byte[]> GenerateLowStockPdfAsync(int threshold)
    {
        ValidateThreshold(threshold);

        var products = await _productRepository.GetLowStockUsingStoredProcedureAsync(threshold);

        var items = products
            .Select(MapToReportItemDto)
            .ToList();

        var summary = BuildSummaryFromProducts(products, threshold);

        return _pdfReportGenerator.GenerateInventoryReportPdf(
            $"Low Stock Report - Below {threshold}",
            items,
            summary);
    }

    // Validates low-stock threshold to prevent invalid reporting rules.
    private static void ValidateThreshold(int threshold)
    {
        if (threshold <= 0)
        {
            throw new ArgumentException("Threshold must be greater than zero.");
        }
    }

    // Builds summary metrics from a specific product set.
    // Used when the report summary should represent filtered data instead of full inventory.
    private static InventorySummaryDto BuildSummaryFromProducts(List<Product> products, int threshold)
    {
        return new InventorySummaryDto
        {
            TotalProducts = products.Count,

            TotalQuantity = products.Sum(p => p.Quantity),

            AveragePrice = products.Any()
                ? Math.Round(products.Average(p => p.PurchasePrice), 2)
                : 0,

            TotalInventoryValue = products.Sum(p => p.Quantity * p.PurchasePrice),

            LowStockProducts = products.Count(p => p.Quantity < threshold)
        };
    }

    // Maps Product entity to report DTO.
    // Adds derived reporting value without exposing the domain entity directly.
    private static InventoryReportItemDto MapToReportItemDto(Product product)
    {
        return new InventoryReportItemDto
        {
            SKU = product.SKU,
            Name = product.Name,
            Size = product.Size,
            Color = product.Color,
            Quantity = product.Quantity,
            PurchasePrice = product.PurchasePrice,
            PurchaseDate = product.PurchaseDate,
            TotalValue = product.Quantity * product.PurchasePrice
        };
    }
}