namespace StockFlow.Application.DTOs.Products;

// This DTO is used when sending product data back to the client.
// We do not return the Product entity directly.
public class ProductResponseDto
{
    // Database-generated primary key.
    public int Id { get; set; }

    // Unique business identifier.
    public string SKU { get; set; } = string.Empty;

    // Product name.
    public string Name { get; set; } = string.Empty;

    // Optional product size.
    public string? Size { get; set; }

    // Optional product color.
    public string? Color { get; set; }

    // Current available quantity.
    public int Quantity { get; set; }

    // Buying price of the product.
    public decimal PurchasePrice { get; set; }

    // Date when the product was purchased.
    public DateTime PurchaseDate { get; set; }
}