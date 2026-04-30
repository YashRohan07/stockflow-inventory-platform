using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.DTOs.Products;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.API.Controllers;

// This controller handles product CRUD API requests.
// Controller should stay thin.
// Business logic is handled by ProductService.
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    // Product service is injected using Dependency Injection.
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: /api/products
    // Example:
    // /api/products?search=shirt&purchaseDateFrom=2026-01-01&sortBy=price&sortOrder=asc&page=1&pageSize=10
    // Admin and Member can view product list.
    [HttpGet]
    [Authorize(Roles = "Admin,Member")]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParametersDto query)
    {
        var products = await _productService.GetAllAsync(query);

        return Ok(new
        {
            success = true,
            message = "Products retrieved successfully.",
            data = products
        });
    }

    // GET: /api/products/{id}
    // Admin and Member can view product details.
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

    // POST: /api/products
    // Only Admin can create product.
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

    // PUT: /api/products/{id}
    // Only Admin can update product.
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

    // DELETE: /api/products/{id}
    // Only Admin can delete product.
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