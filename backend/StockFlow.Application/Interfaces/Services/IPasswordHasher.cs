namespace StockFlow.Application.Interfaces.Services;

// Defines password hashing and verification contract.
// Abstracts hashing algorithm details (e.g., BCrypt, PBKDF2) from the Application layer.
// Ensures consistent and secure password handling across the system.
public interface IPasswordHasher
{
    // Converts a plain-text password into a secure hashed representation.
    // The hashing algorithm should include salting and be resistant to brute-force attacks.
    string HashPassword(string password);

    // Verifies whether a plain-text password matches the stored hash.
    // Used during authentication to validate user credentials.
    bool VerifyPassword(string password, string passwordHash);
}