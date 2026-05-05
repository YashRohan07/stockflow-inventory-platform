using StockFlow.Application.Common;
using StockFlow.Application.DTOs.Products;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Application.Interfaces.Services;
using StockFlow.Application.Validators;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Services;

// Handles product and inventory business logic.
// Coordinates validation, business rules, repository access, and DTO mapping.
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        // Repository abstraction keeps database access separate from business logic.
        _productRepository = productRepository;
    }

    // Retrieves products using search, filtering, sorting, and pagination.
    // Converts domain entities into response DTOs before returning data to the API layer.
    public async Task<PagedResponse<ProductResponseDto>> GetAllAsync(ProductQueryParametersDto query)
    {
        var pagedProducts = await _productRepository.GetAllAsync(query);

        return new PagedResponse<ProductResponseDto>
        {
            Items = pagedProducts.Items
                .Select(MapToResponseDto)
                .ToList(),

            Page = pagedProducts.Page,
            PageSize = pagedProducts.PageSize,
            TotalCount = pagedProducts.TotalCount
        };
    }

    // Retrieves a single product by ID.
    // Not-found handling is done here so controllers remain thin.
    public async Task<ProductResponseDto> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        return MapToResponseDto(product);
    }

    // Creates a new product after validation and SKU uniqueness checks.
    public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
    {
        // Validate input before applying business rules or database operations.
        var validationErrors = CreateProductValidator.Validate(dto);

        if (validationErrors.Any())
        {
            throw new ArgumentException(string.Join(" ", validationErrors));
        }

        // Normalize SKU before duplicate check to avoid whitespace-based duplicates.
        var normalizedSku = dto.SKU.Trim();

        // SKU is treated as a unique business identifier.
        var skuExists = await _productRepository.SkuExistsAsync(normalizedSku);

        if (skuExists)
        {
            throw new InvalidOperationException("A product with this SKU already exists.");
        }

        // Map validated input DTO to domain entity.
        var product = new Product
        {
            SKU = normalizedSku,
            Name = dto.Name.Trim(),
            Size = string.IsNullOrWhiteSpace(dto.Size) ? null : dto.Size.Trim(),
            Color = string.IsNullOrWhiteSpace(dto.Color) ? null : dto.Color.Trim(),
            Quantity = dto.Quantity,
            PurchasePrice = dto.PurchasePrice,
            PurchaseDate = dto.PurchaseDate
        };

        await _productRepository.AddAsync(product);

        return MapToResponseDto(product);
    }

    // Updates an existing product while preserving immutable business identity fields.
    public async Task<ProductResponseDto> UpdateAsync(int id, UpdateProductDto dto)
    {
        // Validate input before loading and mutating the entity.
        var validationErrors = UpdateProductValidator.Validate(dto);

        if (validationErrors.Any())
        {
            throw new ArgumentException(string.Join(" ", validationErrors));
        }

        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        // SKU is intentionally not updated because it represents product business identity.
        product.Name = dto.Name.Trim();
        product.Size = string.IsNullOrWhiteSpace(dto.Size) ? null : dto.Size.Trim();
        product.Color = string.IsNullOrWhiteSpace(dto.Color) ? null : dto.Color.Trim();
        product.Quantity = dto.Quantity;
        product.PurchasePrice = dto.PurchasePrice;
        product.PurchaseDate = dto.PurchaseDate;

        await _productRepository.UpdateAsync(product);

        return MapToResponseDto(product);
    }

    // Deletes an existing product after verifying it exists.
    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        await _productRepository.DeleteAsync(product);
    }

    // Maps domain entity to response DTO.
    // Keeps internal domain model separate from public API response contract.
    private static ProductResponseDto MapToResponseDto(Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id,
            SKU = product.SKU,
            Name = product.Name,
            Size = product.Size,
            Color = product.Color,
            Quantity = product.Quantity,
            PurchasePrice = product.PurchasePrice,
            PurchaseDate = product.PurchaseDate
        };
    }
}