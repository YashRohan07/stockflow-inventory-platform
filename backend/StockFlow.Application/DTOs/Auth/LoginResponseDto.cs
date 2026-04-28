namespace StockFlow.Application.DTOs.Auth;

// This DTO sends login result back to the frontend
public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public LoggedInUserDto User { get; set; } = new();
}