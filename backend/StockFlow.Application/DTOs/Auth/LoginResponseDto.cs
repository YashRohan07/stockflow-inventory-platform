namespace StockFlow.Application.DTOs.Auth;

// Represents the response returned after successful authentication.
// Contains JWT token, expiration info, and safe user details for client usage.
public class LoginResponseDto
{
    // JWT token used for authenticating subsequent API requests
    public string Token { get; set; } = string.Empty;

    // Exact UTC time when the token will expire
    // Used by frontend to handle auto logout or token refresh logic
    public DateTime ExpiresAt { get; set; }

    // Basic user information (non-sensitive) for UI display and role-based behavior
    public LoggedInUserDto User { get; set; } = new();
}