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
    // Instead of writing all configurations here,
    // we automatically load all configurations from the assembly
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This line automatically applies all IEntityTypeConfiguration classes
        // Example: ProductConfiguration, UserConfiguration (if added)
        // This keeps DbContext clean and scalable
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Always call base method
        base.OnModelCreating(modelBuilder);
    }
}