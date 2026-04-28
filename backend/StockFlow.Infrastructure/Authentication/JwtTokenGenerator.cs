// Used for creating and handling JWT tokens
using System.IdentityModel.Tokens.Jwt;

// Used for defining claims (user identity inside token)
using System.Security.Claims;

// Used for encoding the secret key
using System.Text;

// Used to read configuration values (JwtOptions)
using Microsoft.Extensions.Options;

// Used for security key and signing credentials
using Microsoft.IdentityModel.Tokens;

// Importing JwtOptions from Application layer (IMPORTANT FIX)
using StockFlow.Application.Common.Options;

// Importing interface from Application layer
using StockFlow.Application.Interfaces.Services;

// Importing User entity from Domain layer
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Authentication;

// This service is responsible for generating JWT tokens
public class JwtTokenGenerator : IJwtTokenGenerator
{
    // Holds JWT configuration values
    private readonly JwtOptions _jwtOptions;

    // Constructor gets JwtOptions via Dependency Injection
    public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    // Generate JWT token for a logged-in user
    public string GenerateToken(User user)
    {
        // Claims = user identity data stored inside token
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // user id
            new Claim(ClaimTypes.Name, user.Name),                     // name
            new Claim(ClaimTypes.Email, user.Email),                   // email
            new Claim(ClaimTypes.Role, user.Role.ToString())           // role (Admin/Member)
        };

        // Convert SecretKey string into byte array
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        // Create signing credentials (HMAC SHA256)
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // Create JWT token
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,         // who issued token
            audience: _jwtOptions.Audience,     // who can use token
            claims: claims,                     // user data
            expires: GetTokenExpiryTime(),      // expiry time
            signingCredentials: credentials     // security signature
        );

        // Convert token object to string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Returns expiry time based on configuration
    public DateTime GetTokenExpiryTime()
    {
        return DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes);
    }
}