using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Persistence;

// Main EF Core database context for StockFlow.
// Acts as the Infrastructure layer bridge between domain entities and the SQL Server database.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Products table used for product and inventory management.
    public DbSet<Product> Products => Set<Product>();

    // Users table used for authentication and role-based access.
    public DbSet<User> Users => Set<User>();

    // Applies entity configurations using Fluent API.
    // Keeps entity mapping rules separated from DbContext for better maintainability.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Automatically applies all IEntityTypeConfiguration classes in this assembly.
        // Example: ProductConfiguration.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}