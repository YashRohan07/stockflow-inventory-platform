using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockFlow.Application.Common.Options;
using StockFlow.Application.Interfaces.Services;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Authentication;

// Generates signed JWT tokens for authenticated users.
// Implements the Application layer contract while keeping JWT-specific infrastructure details here.
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
    {
        // JwtOptions is loaded from configuration and injected through the options pattern.
        _jwtOptions = jwtOptions.Value;
    }

    // Creates a signed JWT containing the minimum required user identity claims.
    public string GenerateToken(User user)
    {
        // Claims represent user identity and authorization data stored inside the token.
        // Keep claims minimal to avoid exposing unnecessary user information.
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        // Convert configured secret key into a symmetric signing key.
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        // Use HMAC SHA256 to sign the token and prevent tampering.
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: GetTokenExpiryTime(),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Calculates token expiration time using UTC for consistency across environments.
    public DateTime GetTokenExpiryTime()
    {
        return DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes);
    }
}