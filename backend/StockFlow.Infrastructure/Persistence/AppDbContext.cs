// Required for using Entity Framework Core
using Microsoft.EntityFrameworkCore;

// Importing domain entities (Product, User)
using StockFlow.Domain.Entities;

// Namespace for database-related code in Infrastructure layer
namespace StockFlow.Infrastructure.Persistence;

// This is the main database context class
// It acts as a bridge between the application and the database
public class AppDbContext : DbContext
{
    // Constructor receives configuration options
    // These options include database type (SQL Server), connection string, etc.
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Represents the Products table in the database
    // We can use this to query and save Product data
    public DbSet<Product> Products => Set<Product>();

    // Represents the Users table in the database
    // We can use this to query and save User data
    public DbSet<User> Users => Set<User>();

    // This method is used to configure how entities map to database tables
    // We use it to define rules like column types, precision, relationships, etc.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure decimal precision for PurchasePrice
        // This prevents data loss (truncation) in SQL Server
        modelBuilder.Entity<Product>()
            .Property(p => p.PurchasePrice)
            .HasPrecision(18, 2); // total 18 digits, 2 after decimal

        // Always call base method
        base.OnModelCreating(modelBuilder);
    }
}