using StockFlow.Application.Common;
using StockFlow.Application.DTOs.Products;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<PagedResponse<Product>> GetAllAsync(ProductQueryParametersDto query);

    Task<Product?> GetByIdAsync(int id);

    Task<Product?> GetBySkuAsync(string sku);

    Task<bool> SkuExistsAsync(string sku);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Product product);

    Task<List<Product>> GetAllForReportAsync();

    Task<List<Product>> GetLowStockAsync(int threshold);

    Task<List<Product>> GetByPurchaseDateRangeAsync(DateTime from, DateTime to);

    // DB-level aggregation methods for reporting summary.
    Task<int> GetTotalProductsCountAsync();

    Task<int> GetTotalQuantityAsync();

    Task<decimal> GetAveragePriceAsync();

    Task<decimal> GetTotalInventoryValueAsync();

    Task<int> GetLowStockCountAsync(int threshold);
}