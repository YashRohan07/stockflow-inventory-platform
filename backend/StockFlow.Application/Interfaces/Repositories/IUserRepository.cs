using StockFlow.Domain.Entities;

namespace StockFlow.Application.Interfaces.Repositories;

// Defines data access operations related to users.
// Currently minimal because the system only requires user lookup for authentication.
// Can be extended later for user management features (registration, roles, etc.).
public interface IUserRepository
{
    // Retrieves a user by email address.
    // Primary use case: authentication (login flow).
    Task<User?> GetByEmailAsync(string email);
}