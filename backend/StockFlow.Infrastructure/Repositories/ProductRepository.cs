using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Domain.Entities;
using StockFlow.Infrastructure.Persistence;

namespace StockFlow.Infrastructure.Repositories;

// This class contains actual database operations for Product.
// It uses AppDbContext to communicate with SQL Server.
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    // AppDbContext is injected using Dependency Injection.
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get all products.
    // AsNoTracking improves performance for read-only queries.
    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .OrderByDescending(p => p.Id)
            .ToListAsync();
    }

    // Get product by database Id.
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Get product by SKU.
    // SKU comparison is done after trimming input.
    public async Task<Product?> GetBySkuAsync(string sku)
    {
        var normalizedSku = sku.Trim();

        return await _context.Products
            .FirstOrDefaultAsync(p => p.SKU == normalizedSku);
    }

    // Check if SKU already exists.
    public async Task<bool> SkuExistsAsync(string sku)
    {
        var normalizedSku = sku.Trim();

        return await _context.Products
            .AnyAsync(p => p.SKU == normalizedSku);
    }

    // Add new product.
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    // Update existing product.
    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    // Delete existing product.
    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}