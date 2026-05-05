using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Interfaces.Repositories;
using StockFlow.Domain.Entities;
using StockFlow.Infrastructure.Persistence;

namespace StockFlow.Infrastructure.Repositories;

// EF Core implementation of user data access.
// Currently focused on authentication-related user lookup.
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        // DbContext provides access to the Users table.
        _context = context;
    }

    // Retrieves a user by email for login flow.
    // Email should be normalized before reaching this method to keep lookup consistent.
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}