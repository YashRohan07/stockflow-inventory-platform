using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Common;
using StockFlow.Application.DTOs.Products;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Domain.Entities;
using StockFlow.Infrastructure.Persistence;

namespace StockFlow.Infrastructure.Repositories;

// EF Core implementation of product data access.
// Handles product CRUD, listing queries, reporting queries, and inventory aggregation.
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        // DbContext is injected per request and manages database operations.
        _context = context;
    }

    // Retrieves products with search, date filtering, sorting, and pagination.
    // Uses AsNoTracking because list results are read-only and do not need change tracking.
    public async Task<PagedResponse<Product>> GetAllAsync(ProductQueryParametersDto query)
    {
        var productsQuery = _context.Products
            .AsNoTracking()
            .AsQueryable();

        // Search is applied only to SKU and Name for predictable query behavior.
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var searchText = query.Search.Trim();

            productsQuery = productsQuery.Where(p =>
                p.SKU.Contains(searchText) ||
                p.Name.Contains(searchText));
        }

        // Optional purchase date range filtering.
        if (query.PurchaseDateFrom.HasValue)
        {
            productsQuery = productsQuery.Where(p =>
                p.PurchaseDate >= query.PurchaseDateFrom.Value);
        }

        if (query.PurchaseDateTo.HasValue)
        {
            productsQuery = productsQuery.Where(p =>
                p.PurchaseDate <= query.PurchaseDateTo.Value);
        }

        // Count is calculated before pagination so frontend can build pagination controls.
        var totalCount = await productsQuery.CountAsync();

        var sortBy = query.SortBy.Trim().ToLower();
        var sortOrder = query.SortOrder.Trim().ToLower();
        var isAscending = sortOrder == "asc";

        // Whitelisted sorting avoids unsafe dynamic query construction.
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

    // Retrieves a product by database ID for details, update, and delete operations.
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Retrieves a product by SKU business identifier.
    public async Task<Product?> GetBySkuAsync(string sku)
    {
        var normalizedSku = sku.Trim();

        return await _context.Products
            .FirstOrDefaultAsync(p => p.SKU == normalizedSku);
    }

    // Checks whether a SKU already exists before creating a product.
    public async Task<bool> SkuExistsAsync(string sku)
    {
        var normalizedSku = sku.Trim();

        return await _context.Products
            .AnyAsync(p => p.SKU == normalizedSku);
    }

    // Adds a new product and immediately persists changes.
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    // Updates an existing tracked/detached product entity and persists changes.
    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    // Deletes an existing product and persists changes.
    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    // Returns all products for full inventory reporting.
    // Ordered by latest purchase date to show recent stock first.
    public async Task<List<Product>> GetAllForReportAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();
    }

    // Returns products below the selected low-stock threshold.
    public async Task<List<Product>> GetLowStockAsync(int threshold)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.Quantity < threshold)
            .OrderBy(p => p.Quantity)
            .ToListAsync();
    }

    // Uses SQL Server stored procedure through EF Core raw SQL.
    // Demonstrates database-side reporting optimization while keeping access behind repository abstraction.
    public async Task<List<Product>> GetLowStockUsingStoredProcedureAsync(int threshold)
    {
        return await _context.Products
            .FromSqlRaw("EXEC dbo.GetLowStockProducts @Threshold = {0}", threshold)
            .AsNoTracking()
            .ToListAsync();
    }

    // Returns products purchased within a specific date range for reporting.
    public async Task<List<Product>> GetByPurchaseDateRangeAsync(DateTime from, DateTime to)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.PurchaseDate >= from && p.PurchaseDate <= to)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();
    }

    // Returns total number of products for summary metrics.
    public async Task<int> GetTotalProductsCountAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .CountAsync();
    }

    // Returns total available stock quantity across all products.
    public async Task<int> GetTotalQuantityAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .SumAsync(p => p.Quantity);
    }

    // Returns average purchase price.
    // Explicit empty-check prevents AverageAsync from failing on empty tables.
    public async Task<decimal> GetAveragePriceAsync()
    {
        var hasProducts = await _context.Products.AnyAsync();

        if (!hasProducts)
        {
            return 0;
        }

        return await _context.Products
            .AsNoTracking()
            .AverageAsync(p => p.PurchasePrice);
    }

    // Returns total inventory value across all products.
    public async Task<decimal> GetTotalInventoryValueAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .SumAsync(p => p.Quantity * p.PurchasePrice);
    }

    // Returns number of products below the selected low-stock threshold.
    public async Task<int> GetLowStockCountAsync(int threshold)
    {
        return await _context.Products
            .AsNoTracking()
            .CountAsync(p => p.Quantity < threshold);
    }
}