namespace StockFlow.Application.Common.Options;

// This class maps JWT settings from appsettings.json
public class JwtOptions
{
    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public int ExpiryMinutes { get; set; }
}