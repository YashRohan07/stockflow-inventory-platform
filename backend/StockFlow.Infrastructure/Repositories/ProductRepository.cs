using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Common;
using StockFlow.Application.DTOs.Products;
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

    // Get products with search, filter, sort, and pagination.
    // Query logic stays in repository because it is database-related work.
    public async Task<PagedResponse<Product>> GetAllAsync(ProductQueryParametersDto query)
    {
        // Start with IQueryable so EF Core can build one optimized SQL query.
        var productsQuery = _context.Products
            .AsNoTracking()
            .AsQueryable();

        // Search by SKU or Name.
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var searchText = query.Search.Trim();

            productsQuery = productsQuery.Where(p =>
                p.SKU.Contains(searchText) ||
                p.Name.Contains(searchText));
        }

        // Filter by purchase date from.
        if (query.PurchaseDateFrom.HasValue)
        {
            productsQuery = productsQuery.Where(p =>
                p.PurchaseDate >= query.PurchaseDateFrom.Value);
        }

        // Filter by purchase date to.
        if (query.PurchaseDateTo.HasValue)
        {
            productsQuery = productsQuery.Where(p =>
                p.PurchaseDate <= query.PurchaseDateTo.Value);
        }

        // Count before pagination.
        var totalCount = await productsQuery.CountAsync();

        // Normalize sorting values.
        var sortBy = query.SortBy.Trim().ToLower();
        var sortOrder = query.SortOrder.Trim().ToLower();
        var isAscending = sortOrder == "asc";

        // Apply sorting.
        productsQuery = sortBy switch
        {
            "name" => isAscending
                ? productsQuery.OrderBy(p => p.Name)
                : productsQuery.OrderByDescending(p => p.Name),

            "price" => isAscending
                ? productsQuery.OrderBy(p => p.PurchasePrice)
                : productsQuery.OrderByDescending(p => p.PurchasePrice),

            "quantity" => isAscending
                ? productsQuery.OrderBy(p => p.Quantity)
                : productsQuery.OrderByDescending(p => p.Quantity),

            "purchasedate" => isAscending
                ? productsQuery.OrderBy(p => p.PurchaseDate)
                : productsQuery.OrderByDescending(p => p.PurchaseDate),

            _ => productsQuery.OrderByDescending(p => p.Id)
        };

        // Apply pagination.
        var items = await productsQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PagedResponse<Product>
        {
            Items = items,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount
        };
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