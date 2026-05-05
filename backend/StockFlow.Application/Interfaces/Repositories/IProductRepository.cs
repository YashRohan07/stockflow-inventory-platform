using StockFlow.Application.Common;
using StockFlow.Application.DTOs.Products;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Interfaces.Repositories;

// Defines product data access operations required by the Application layer.
// The Application layer depends on this abstraction, while Infrastructure provides the actual EF Core implementation.
public interface IProductRepository
{
    // Retrieves products with search, filtering, sorting, and pagination support.
    Task<PagedResponse<Product>> GetAllAsync(ProductQueryParametersDto query);

    // Retrieves a single product by database-generated ID.
    Task<Product?> GetByIdAsync(int id);

    // Retrieves a single product by SKU business identifier.
    Task<Product?> GetBySkuAsync(string sku);

    // Checks SKU uniqueness before creating a product.
    Task<bool> SkuExistsAsync(string sku);

    // Persists a new product entity.
    Task AddAsync(Product product);

    // Updates an existing product entity.
    Task UpdateAsync(Product product);

    // Deletes an existing product entity.
    Task DeleteAsync(Product product);

    // Returns all products needed for full inventory report generation.
    Task<List<Product>> GetAllForReportAsync();

    // Returns products where quantity is below or equal to the given threshold.
    Task<List<Product>> GetLowStockAsync(int threshold);

    // Returns low-stock products using a database stored procedure for optimization/demo purposes.
    Task<List<Product>> GetLowStockUsingStoredProcedureAsync(int threshold);

    // Returns products purchased within a specific date range.
    Task<List<Product>> GetByPurchaseDateRangeAsync(DateTime from, DateTime to);

    // Aggregation methods used for inventory summary/reporting metrics.
    Task<int> GetTotalProductsCountAsync();

    Task<int> GetTotalQuantityAsync();

    Task<decimal> GetAveragePriceAsync();

    Task<decimal> GetTotalInventoryValueAsync();

    Task<int> GetLowStockCountAsync(int threshold);
}