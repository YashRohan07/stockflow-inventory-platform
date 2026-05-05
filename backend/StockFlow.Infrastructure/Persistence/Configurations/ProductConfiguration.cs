using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Persistence.Configurations;

// Configures the Product entity for database mapping using EF Core Fluent API.
// Defines table structure, constraints, indexes, and column rules.
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Map to "Products" table and define database-level constraints
        builder.ToTable("Products", table =>
        {
            // Enforce non-negative quantity at database level (data integrity safeguard)
            table.HasCheckConstraint(
                "CK_Products_Quantity_NonNegative",
                "[Quantity] >= 0");

            // Enforce non-negative purchase price at database level
            table.HasCheckConstraint(
                "CK_Products_PurchasePrice_NonNegative",
                "[PurchasePrice] >= 0");
        });

        // Primary key
        builder.HasKey(p => p.Id);

        // SKU configuration (business identifier)
        builder.Property(p => p.SKU)
            .IsRequired()
            .HasMaxLength(50);

        // Unique index ensures SKU uniqueness at DB level
        builder.HasIndex(p => p.SKU)
            .IsUnique();

        // Name configuration
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        // Index to improve search/filter performance on product name
        builder.HasIndex(p => p.Name);

        // Optional attributes with length constraints
        builder.Property(p => p.Size)
            .HasMaxLength(50);

        builder.Property(p => p.Color)
            .HasMaxLength(50);

        // Quantity configuration
        builder.Property(p => p.Quantity)
            .IsRequired();

        // Index for faster filtering/sorting by quantity
        builder.HasIndex(p => p.Quantity);

        // Price configuration with fixed precision
        builder.Property(p => p.PurchasePrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Purchase date configuration
        builder.Property(p => p.PurchaseDate)
            .IsRequired();

        // Index for date-based queries (reporting, filtering)
        builder.HasIndex(p => p.PurchaseDate);
    }
}