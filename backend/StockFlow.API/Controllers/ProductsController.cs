using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.DTOs.Products;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.API.Controllers;

// Handles product and inventory-related API operations.
// Controller remains thin by delegating business rules, validation flow, and data access coordination to ProductService.
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    // Application service responsible for product CRUD, search, filter, sort, and pagination logic.
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        // Dependency Injection keeps the controller loosely coupled and easier to test.
        _productService = productService;
    }

    // Endpoint: GET /api/products
    // Purpose: Retrieve products with optional search, date filtering, sorting, and pagination.
    // Access: Admin and Member can view product/inventory data.
    // Example: /api/products?search=shirt&purchaseDateFrom=2026-01-01&sortBy=price&sortOrder=asc&page=1&pageSize=10
    [HttpGet]
    [Authorize(Roles = "Admin,Member")]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParametersDto query)
    {
        // Query processing is handled in the service layer to keep API logic clean.
        var products = await _productService.GetAllAsync(query);

        return Ok(new
        {
            success = true,
            message = "Products retrieved successfully.",
            data = products
        });
    }

    // Endpoint: GET /api/products/{id}
    // Purpose: Retrieve a single product by ID.
    // Access: Admin and Member can view product details.
    // Note: The route constraint ensures only integer IDs match this endpoint.
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Member")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        return Ok(new
        {
            success = true,
            message = "Product retrieved successfully.",
            data = product
        });
    }

    // Endpoint: POST /api/products
    // Purpose: Create a new product record.
    // Access: Only Admin can create product/inventory entries.
    // Note: DTO validation should run before service execution if validation pipeline is configured.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
    {
        var createdProduct = await _productService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetProductById),
            new { id = createdProduct.Id },
            new
            {
                success = true,
                message = "Product created successfully.",
                data = createdProduct
            });
    }

    // Endpoint: PUT /api/products/{id}
    // Purpose: Update an existing product record.
    // Access: Only Admin can modify product/inventory data.
    // Note: Not-found and validation errors should be handled by service layer and global exception middleware.
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto dto)
    {
        var updatedProduct = await _productService.UpdateAsync(id, dto);

        return Ok(new
        {
            success = true,
            message = "Product updated successfully.",
            data = updatedProduct
        });
    }

    // Endpoint: DELETE /api/products/{id}
    // Purpose: Delete a product record.
    // Access: Only Admin can remove product/inventory data.
    // Note: Delete rules and not-found handling are delegated to the service layer.
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteAsync(id);

        return Ok(new
        {
            success = true,
            message = "Product deleted successfully."
        });
    }
}