using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Interfaces.Services;
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;

namespace StockFlow.Infrastructure.Persistence;

// Seeds initial/default data into the database.
// Primarily used for development and testing environments.
public class DatabaseSeeder
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public DatabaseSeeder(AppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    // Seeds default users if the Users table is empty.
    // Safe to run multiple times (idempotent behavior).
    public async Task SeedAsync()
    {
        // Prevent duplicate seeding
        if (await _context.Users.AnyAsync())
        {
            return;
        }

        // Default Admin user (for initial system access)
        var adminUser = new User
        {
            Name = "System Admin",
            Email = "admin@stockflow.com",

            // Password is hashed before storage (never store plain-text passwords)
            // NOTE: Hardcoded credentials are for development only and should not be used in production.
            PasswordHash = _passwordHasher.HashPassword("Admin@786"),

            Role = UserRole.Admin,
            CreatedAt = DateTime.UtcNow
        };

        // Default Member user (for testing role-based access)
        var memberUser = new User
        {
            Name = "Member",
            Email = "member@stockflow.com",

            // NOTE: Development-only credentials
            PasswordHash = _passwordHasher.HashPassword("Member@787"),

            Role = UserRole.Member,
            CreatedAt = DateTime.UtcNow
        };

        // Add users in a single batch operation
        await _context.Users.AddRangeAsync(adminUser, memberUser);

        // Persist changes to the database
        await _context.SaveChangesAsync();
    }
}