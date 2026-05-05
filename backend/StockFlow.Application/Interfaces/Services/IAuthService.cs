using StockFlow.Application.DTOs.Auth;

namespace StockFlow.Application.Interfaces.Services;

// Defines authentication-related business operations.
// Responsible for validating credentials and generating authentication tokens.
// Acts as the bridge between API layer and authentication infrastructure (JWT, hashing, etc.).
public interface IAuthService
{
    // Validates user credentials and returns authentication result.
    // Flow:
    // 1. Validate input (handled by validation pipeline)
    // 2. Retrieve user from repository
    // 3. Verify password (hash comparison)
    // 4. Generate JWT token
    // 5. Return token + user info to client
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}