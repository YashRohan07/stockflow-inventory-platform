namespace StockFlow.Application.Common.Options;

// Represents JWT configuration settings loaded from appsettings.json.
// Used for token generation and validation across authentication components.
public class JwtOptions
{
    // Token issuer (identifies the API that generated the token)
    public string Issuer { get; set; } = string.Empty;

    // Token audience (identifies the intended client, e.g., frontend app)
    public string Audience { get; set; } = string.Empty;

    // Secret key used to sign JWT tokens.
    // Must be kept secure and should be moved to environment variables in production.
    public string SecretKey { get; set; } = string.Empty;

    // Token expiration duration in minutes.
    // Defines how long a JWT remains valid after issuance.
    public int ExpiryMinutes { get; set; }
}