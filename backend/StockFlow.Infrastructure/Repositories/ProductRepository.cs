using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Common;
using StockFlow.Application.DTOs.Products;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Domain.Entities;
using StockFlow.Infrastructure.Persistence;

namespace StockFlow.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<Product>> GetAllAsync(ProductQueryParametersDto query)
    {
        var productsQuery = _context.Products
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var searchText = query.Search.Trim();

            productsQuery = productsQuery.Where(p =>
                p.SKU.Contains(searchText) ||
                p.Name.Contains(searchText));
        }

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

        var totalCount = await productsQuery.CountAsync();

        var sortBy = query.SortBy.Trim().ToLower();
        var sortOrder = query.SortOrder.Trim().ToLower();
        var isAscending = sortOrder == "asc";

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

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        var normalizedSku = sku.Trim();

        return await _context.Products
            .FirstOrDefaultAsync(p => p.SKU == normalizedSku);
    }

    public async Task<bool> SkuExistsAsync(string sku)
    {
        var normalizedSku = sku.Trim();

        return await _context.Products
            .AnyAsync(p => p.SKU == normalizedSku);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAllForReportAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();
    }

    public async Task<List<Product>> GetLowStockAsync(int threshold)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.Quantity < threshold)
            .OrderBy(p => p.Quantity)
            .ToListAsync();
    }

    public async Task<List<Product>> GetByPurchaseDateRangeAsync(DateTime from, DateTime to)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.PurchaseDate >= from && p.PurchaseDate <= to)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();
    }

    public async Task<int> GetTotalProductsCountAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .CountAsync();
    }

    public async Task<int> GetTotalQuantityAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .SumAsync(p => p.Quantity);
    }

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

    public async Task<decimal> GetTotalInventoryValueAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .SumAsync(p => p.Quantity * p.PurchasePrice);
    }

    public async Task<int> GetLowStockCountAsync(int threshold)
    {
        return await _context.Products
            .AsNoTracking()
            .CountAsync(p => p.Quantity < threshold);
    }
}