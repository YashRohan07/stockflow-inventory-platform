namespace StockFlow.Domain.Entities
{
    // Product entity represents a product/inventory item in the system.
    // This class maps to the Products table in the database.
    public class Product : BaseEntity
    {
        // SKU means Stock Keeping Unit.
        // This is a user-provided unique business identifier.
        // Example: SKU-001, SHIRT-BLK-M, PANT-RED-32
        public string SKU { get; set; } = string.Empty;

        // Product name.
        // Example: T-Shirt, Jeans, Shirt
        public string Name { get; set; } = string.Empty;

        // Product size.
        // Example: S, M, L, XL, 32, 34
        public string? Size { get; set; }

        // Product color.
        // Example: Black, White, Blue
        public string? Color { get; set; }

        // Available stock quantity.
        // Quantity should not be negative.
        public int Quantity { get; set; }

        // Product purchase price.
        // This is the buying price of the product.
        public decimal PurchasePrice { get; set; }

        // Product purchase date.
        // This helps us track when the product was purchased.
        public DateTime PurchaseDate { get; set; }
    }
}