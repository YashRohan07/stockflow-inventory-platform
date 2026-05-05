namespace StockFlow.Application.DTOs.Auth;

// Represents login request payload from the client.
// Contains user credentials used for authentication.
// Note: Validation (required fields, format) should be handled via FluentValidation or model validation.
public class LoginRequestDto
{
    // User email used as the unique identifier for login
    public string Email { get; set; } = string.Empty;

    // Plain-text password sent by the client (will be hashed and verified in the service layer)
    // Must never be logged or returned in any response
    public string Password { get; set; } = string.Empty;
}