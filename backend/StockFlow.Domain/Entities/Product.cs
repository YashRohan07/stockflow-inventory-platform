namespace StockFlow.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    public string SKU { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal PurchasePrice { get; set; }

    public DateTime PurchaseDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}