namespace StockFlow.Application.DTOs.Auth;

// This DTO represents the logged-in user's basic information
public class LoggedInUserDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}