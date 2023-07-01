namespace Pro.Modular.Shared.Models;

public class JwtOptions
{
    public required string AuthorizationUrl { get; set; }
    public required string TokenUrl { get; set; }
    public required string ApiScope { get; set; }
    public required string OpenIdClientId { get; set; }
}