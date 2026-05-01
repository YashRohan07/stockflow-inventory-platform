using StockFlow.Application.DTOs.Reports;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Application.Interfaces.Services;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Services;

public class ReportService : IReportService
{
    private const int DefaultLowStockThreshold = 20;

    private readonly IProductRepository _productRepository;
    private readonly IPdfReportGenerator _pdfReportGenerator;

    public ReportService(
        IProductRepository productRepository,
        IPdfReportGenerator pdfReportGenerator)
    {
        _productRepository = productRepository;
        _pdfReportGenerator = pdfReportGenerator;
    }

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

    public async Task<List<InventoryReportItemDto>> GetFullInventoryReportAsync()
    {
        var products = await _productRepository.GetAllForReportAsync();

        return products
            .Select(MapToReportItemDto)
            .ToList();
    }

    public async Task<List<InventoryReportItemDto>> GetLowStockReportAsync(int threshold)
    {
        ValidateThreshold(threshold);

        var products = await _productRepository.GetLowStockAsync(threshold);

        return products
            .Select(MapToReportItemDto)
            .ToList();
    }

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

    public async Task<byte[]> GenerateLowStockPdfAsync(int threshold)
    {
        ValidateThreshold(threshold);

        var products = await _productRepository.GetLowStockAsync(threshold);

        var items = products
            .Select(MapToReportItemDto)
            .ToList();

        var summary = BuildSummaryFromProducts(products, threshold);

        return _pdfReportGenerator.GenerateInventoryReportPdf(
            $"Low Stock Report - Below {threshold}",
            items,
            summary);
    }

    private static void ValidateThreshold(int threshold)
    {
        if (threshold <= 0)
        {
            throw new ArgumentException("Threshold must be greater than zero.");
        }
    }

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