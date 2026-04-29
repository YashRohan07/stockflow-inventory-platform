namespace StockFlow.Application.DTOs.Products;

// This DTO is used when updating an existing product.
// SKU is not included because SKU is a business identity.
public class UpdateProductDto
{
    // Product name is required.
    public string Name { get; set; } = string.Empty;

    // Size is optional.
    public string? Size { get; set; }

    // Color is optional.
    public string? Color { get; set; }

    // Quantity means available stock.
    // It cannot be negative.
    public int Quantity { get; set; }

    // Purchase price means buying price.
    // It cannot be negative.
    public decimal PurchasePrice { get; set; }

    // Purchase date means when the product was purchased.
    public DateTime PurchaseDate { get; set; }
}