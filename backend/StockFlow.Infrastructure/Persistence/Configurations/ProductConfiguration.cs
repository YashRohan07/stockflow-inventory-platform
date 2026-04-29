using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Persistence.Configurations
{
    // This class contains database rules for the Product entity.
    // We keep database configuration separate to keep AppDbContext clean.
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Table name
            builder.ToTable("Products");

            // Primary key
            builder.HasKey(p => p.Id);

            // SKU is required and must be unique.
            // SKU is a business identifier, so duplicate SKU should not be allowed.
            builder.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.SKU)
                .IsUnique();

            // Product name is required.
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            // Size is optional.
            builder.Property(p => p.Size)
                .HasMaxLength(50);

            // Color is optional.
            builder.Property(p => p.Color)
                .HasMaxLength(50);

            // Quantity is required.
            // Database rule: Quantity cannot be less than 0.
            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.HasCheckConstraint("CK_Products_Quantity_NonNegative", "[Quantity] >= 0");

            // Purchase price is required.
            // decimal(18,2) means 18 total digits and 2 digits after decimal point.
            builder.Property(p => p.PurchasePrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasCheckConstraint("CK_Products_PurchasePrice_NonNegative", "[PurchasePrice] >= 0");

            // Purchase date is required.
            builder.Property(p => p.PurchaseDate)
                .IsRequired();
        }
    }
}