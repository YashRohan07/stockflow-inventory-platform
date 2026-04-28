using StockFlow.Application.DTOs.Auth;

namespace StockFlow.Application.Interfaces.Services;

// This interface defines authentication-related business operations
public interface IAuthService
{
    // Handles login request and returns JWT token with user info
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}