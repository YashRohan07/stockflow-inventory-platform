using StockFlow.Domain.Entities;

namespace StockFlow.Application.Interfaces.Repositories;

// This interface defines product database operations.
// Application layer only knows the interface, not the database implementation.
public interface IProductRepository
{
    // Get all products from database.
    Task<List<Product>> GetAllAsync();

    // Get a single product by database Id.
    Task<Product?> GetByIdAsync(int id);

    // Get a single product by SKU.
    // SKU is a unique business identifier.
    Task<Product?> GetBySkuAsync(string sku);

    // Check if a SKU already exists.
    // This is useful before creating a new product.
    Task<bool> SkuExistsAsync(string sku);

    // Add a new product to database.
    Task AddAsync(Product product);

    // Update an existing product in database.
    Task UpdateAsync(Product product);

    // Delete an existing product from database.
    Task DeleteAsync(Product product);
}