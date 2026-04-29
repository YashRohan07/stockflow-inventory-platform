using StockFlow.Application.DTOs.Products;

namespace StockFlow.Application.Interfaces.Services;

// This interface defines product business operations.
// Controllers will use this service interface.
public interface IProductService
{
    // Get all products.
    Task<List<ProductResponseDto>> GetAllAsync();

    // Get one product by database Id.
    Task<ProductResponseDto> GetByIdAsync(int id);

    // Create a new product.
    Task<ProductResponseDto> CreateAsync(CreateProductDto dto);

    // Update an existing product.
    Task<ProductResponseDto> UpdateAsync(int id, UpdateProductDto dto);

    // Delete an existing product.
    Task DeleteAsync(int id);
}