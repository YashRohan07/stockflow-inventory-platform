using StockFlow.Application.Common;
using StockFlow.Application.DTOs.Products;

namespace StockFlow.Application.Interfaces.Services;

// Defines product and inventory business operations.
// Acts as the Application layer boundary between API controllers and data access repositories.
public interface IProductService
{
    // Retrieves products using search, filtering, sorting, and pagination.
    // Returns DTOs instead of domain entities to keep the API contract stable.
    Task<PagedResponse<ProductResponseDto>> GetAllAsync(ProductQueryParametersDto query);

    // Retrieves a single product by database-generated ID.
    // Not-found handling should be performed in the service implementation.
    Task<ProductResponseDto> GetByIdAsync(int id);

    // Creates a new product record.
    // Business rules such as SKU uniqueness and validation are handled before persistence.
    Task<ProductResponseDto> CreateAsync(CreateProductDto dto);

    // Updates an existing product record.
    // SKU is intentionally not updated because it is treated as a business identity.
    Task<ProductResponseDto> UpdateAsync(int id, UpdateProductDto dto);

    // Deletes an existing product record.
    // Service layer controls not-found checks and delete rules.
    Task DeleteAsync(int id);
}