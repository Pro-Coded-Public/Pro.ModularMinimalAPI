namespace Pro.Modular.Shared.Models;

public class RootObject
{
    public required JwtOptions AzureAd { get; set; }
    public required Logging Logging { get; set; }
}

public class JwtOptions
{
    public required string AuthorizationUrl { get; set; }
    public required string TokenUrl { get; set; }
    public required string ApiScope { get; set; }
    public required string OpenIdClientId { get; set; }
}

public class Logging
{
    public required LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    public required string Default { get; set; }
    public required string Microsoft_AspNetCore { get; set; }
}