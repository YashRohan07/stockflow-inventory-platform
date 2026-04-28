using Microsoft.AspNetCore.Identity;
using StockFlow.Application.Interfaces.Services;

namespace StockFlow.Infrastructure.Authentication;

// This service handles password hashing and password verification
public class PasswordHasherService : IPasswordHasher
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    // Converts plain password into hashed password
    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(new object(), password);
    }

    // Verifies plain password against stored hashed password
    public bool VerifyPassword(string password, string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            new object(),
            passwordHash,
            password
        );

        return result == PasswordVerificationResult.Success;
    }
}