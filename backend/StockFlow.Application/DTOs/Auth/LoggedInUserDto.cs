namespace StockFlow.Application.DTOs.Auth;

// Represents user information returned after successful authentication.
// This is intentionally limited to safe, non-sensitive fields for client consumption.
public class LoggedInUserDto
{
    // Unique identifier of the authenticated user
    public int Id { get; set; }

    // Display name of the user
    public string Name { get; set; } = string.Empty;

    // User email address (used for identification and UI display)
    public string Email { get; set; } = string.Empty;

    // Role assigned to the user (used for frontend role-based UI control)
    public string Role { get; set; } = string.Empty;
}