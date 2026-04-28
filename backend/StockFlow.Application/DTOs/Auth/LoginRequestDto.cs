namespace StockFlow.Application.DTOs.Auth;

// This DTO receives login data from the frontend
public class LoginRequestDto
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}