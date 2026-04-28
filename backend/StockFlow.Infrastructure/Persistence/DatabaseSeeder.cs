// Required for async database operations like AnyAsync()
using Microsoft.EntityFrameworkCore;

// Importing password hashing interface from Application layer
using StockFlow.Application.Interfaces.Services;

// Importing User entity and UserRole enum from Domain layer
using StockFlow.Domain.Entities;
using StockFlow.Domain.Enums;

// Namespace for persistence-related logic in Infrastructure layer
namespace StockFlow.Infrastructure.Persistence;

// This class is responsible for seeding initial/default data into the database
public class DatabaseSeeder
{
    // Database context to interact with database tables
    private readonly AppDbContext _context;

    // Password hasher to securely hash user passwords before saving
    private readonly IPasswordHasher _passwordHasher;

    // Constructor for dependency injection
    public DatabaseSeeder(AppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    // This method runs when the application starts
    // It inserts default users if the Users table is empty
    public async Task SeedAsync()
    {
        // Check if any user already exists in the database
        // If yes, do nothing (avoid duplicate seeding)
        if (await _context.Users.AnyAsync())
        {
            return;
        }

        // Create default Admin user
        var adminUser = new User
        {
            Name = "System Admin",
            Email = "admin@stockflow.com",

            // Hashing password before storing
            PasswordHash = _passwordHasher.HashPassword("Admin@786"),

            Role = UserRole.Admin,

            // Set current UTC time as creation time
            CreatedAt = DateTime.UtcNow
        };

        // Create default Member user
        var memberUser = new User
        {
            Name = "Member",
            Email = "member@stockflow.com",

            // Hashing password before storing
            PasswordHash = _passwordHasher.HashPassword("Member@787"),

            Role = UserRole.Member,

            CreatedAt = DateTime.UtcNow
        };

        // Add both users to database
        await _context.Users.AddRangeAsync(adminUser, memberUser);

        // Save changes to database
        await _context.SaveChangesAsync();
    }
}