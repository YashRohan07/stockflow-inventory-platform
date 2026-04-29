using StockFlow.Application.DTOs.Products;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Application.Interfaces.Services;
using StockFlow.Application.Validators;
using StockFlow.Domain.Entities;

namespace StockFlow.Application.Services;

// ProductService contains product business logic.
// It validates input, checks business rules, and uses repository for database work.
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    // Repository is injected using Dependency Injection.
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    // Get all products and convert them to response DTOs.
    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products
            .Select(MapToResponseDto)
            .ToList();
    }

    // Get a single product by Id.
    public async Task<ProductResponseDto> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        return MapToResponseDto(product);
    }

    // Create a new product.
    public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
    {
        // Validate input first.
        var validationErrors = CreateProductValidator.Validate(dto);

        if (validationErrors.Any())
        {
            throw new ArgumentException(string.Join(" ", validationErrors));
        }

        // Normalize SKU before checking duplicate.
        var normalizedSku = dto.SKU.Trim();

        // SKU must be unique.
        var skuExists = await _productRepository.SkuExistsAsync(normalizedSku);

        if (skuExists)
        {
            throw new InvalidOperationException("A product with this SKU already exists.");
        }

        // Convert DTO to entity.
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

    // Update an existing product.
    public async Task<ProductResponseDto> UpdateAsync(int id, UpdateProductDto dto)
    {
        // Validate input first.
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

        // SKU is not updated because it is a business identifier.
        product.Name = dto.Name.Trim();
        product.Size = string.IsNullOrWhiteSpace(dto.Size) ? null : dto.Size.Trim();
        product.Color = string.IsNullOrWhiteSpace(dto.Color) ? null : dto.Color.Trim();
        product.Quantity = dto.Quantity;
        product.PurchasePrice = dto.PurchasePrice;
        product.PurchaseDate = dto.PurchaseDate;

        await _productRepository.UpdateAsync(product);

        return MapToResponseDto(product);
    }

    // Delete an existing product.
    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        await _productRepository.DeleteAsync(product);
    }

    // Convert Product entity to ProductResponseDto.
    // This keeps entity and API response separate.
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