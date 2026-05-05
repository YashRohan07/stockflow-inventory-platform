namespace StockFlow.Domain.Entities
{
    // Represents a product within the inventory system.
    // This is a core domain entity and maps to the Products table.
    public class Product : BaseEntity
    {
        // Stock Keeping Unit (SKU) - unique business identifier.
        // Immutable after creation and must remain unique across the system.
        public string SKU { get; set; } = string.Empty;

        // Product name used for identification and search.
        public string Name { get; set; } = string.Empty;

        // Optional size attribute (domain-specific classification).
        public string? Size { get; set; }

        // Optional color attribute.
        public string? Color { get; set; }

        // Available stock quantity.
        // Domain constraint: must be >= 0.
        public int Quantity { get; set; }

        // Purchase (cost) price per unit.
        // Domain constraint: must be >= 0.
        public decimal PurchasePrice { get; set; }

        // Date when the product was added/purchased in inventory.
        // Should be handled as UTC for consistency across systems.
        public DateTime PurchaseDate { get; set; }
    }
}