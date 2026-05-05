using Microsoft.AspNetCore.Identity;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.Infrastructure.Authentication;

// Provides password hashing and verification using ASP.NET Identity's built-in PasswordHasher.
// Abstracts hashing implementation from the Application layer.
public class PasswordHasherService : IPasswordHasher
{
    // Uses ASP.NET Identity's secure hashing (PBKDF2 with salt by default)
    private readonly PasswordHasher<object> _passwordHasher = new();

    // Converts a plain-text password into a hashed value.
    // Hash includes salt and is safe to store in the database.
    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(new object(), password);
    }

    // Verifies whether the provided password matches the stored hash.
    // Returns true only when verification succeeds.
    public bool VerifyPassword(string password, string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            new object(),
            passwordHash,
            password
        );

        // Note: SuccessRehashNeeded is also considered valid in some systems.
        // Currently treated as false to keep logic simple.
        return result == PasswordVerificationResult.Success;
    }
}