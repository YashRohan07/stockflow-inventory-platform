namespace StockFlow.Application.Interfaces.Services;

// This interface defines password hashing and verification rules
public interface IPasswordHasher
{
    // Converts a plain password into a secure hashed password
    string HashPassword(string password);

    // Checks if the plain password matches the stored hashed password
    bool VerifyPassword(string password, string passwordHash);
}