using StockFlow.Domain.Entities;

namespace StockFlow.Application.Interfaces.Services;

// This interface defines JWT token generation contract
public interface IJwtTokenGenerator
{
    // Generates JWT token for a valid logged-in user
    string GenerateToken(User user);

    // Calculates token expiration time
    DateTime GetTokenExpiryTime();
}