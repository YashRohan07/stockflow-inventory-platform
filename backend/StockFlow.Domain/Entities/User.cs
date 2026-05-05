using StockFlow.Domain.Enums;

namespace StockFlow.Domain.Entities;

// Represents a system user for authentication and authorization.
// Contains identity, credential (hashed), and role-based access information.
public class User
{
    // Primary key (should ideally inherit from BaseEntity for consistency)
    public int Id { get; set; }

    // Display name of the user
    public string Name { get; set; } = string.Empty;

    // Unique email used for authentication
    public string Email { get; set; } = string.Empty;

    // Secure hashed password (never store plain-text passwords)
    public string PasswordHash { get; set; } = string.Empty;

    // Role used for authorization (Admin, Member, etc.)
    public UserRole Role { get; set; }

    // Timestamp when the user account was created
    // Defaults to UTC to ensure consistency across systems
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}