using StockFlow.Domain.Entities;

namespace StockFlow.Application.Interfaces.Services;

// Defines the contract for JWT token creation.
// The Application layer depends on this abstraction, while the actual JWT implementation stays in Infrastructure.
public interface IJwtTokenGenerator
{
    // Generates a signed JWT token for an authenticated user.
    // Token should include only required claims such as user ID, email, and role.
    string GenerateToken(User user);

    // Returns the calculated token expiration time.
    // Keeps token expiry logic consistent between token generation and login response.
    DateTime GetTokenExpiryTime();
}