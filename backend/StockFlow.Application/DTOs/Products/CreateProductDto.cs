namespace StockFlow.Application.DTOs.Products;

// This DTO is used when creating a new product.
// The client sends this data to the API.
public class CreateProductDto
{
    // SKU is provided by the user.
    // It must be unique in the database.
    public string SKU { get; set; } = string.Empty;

    // Product name is required.
    public string Name { get; set; } = string.Empty;

    // Size is optional.
    // Example: S, M, L, XL, 32, 34
    public string? Size { get; set; }

    // Color is optional.
    // Example: Black, White, Blue
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