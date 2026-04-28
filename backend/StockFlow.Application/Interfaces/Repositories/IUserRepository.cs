using StockFlow.Domain.Entities;

namespace StockFlow.Application.Interfaces.Repositories;

// This interface defines user-related data access operations
public interface IUserRepository
{
    // Get user by email (used for login)
    Task<User?> GetByEmailAsync(string email);
}