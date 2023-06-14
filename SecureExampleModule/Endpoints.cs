using Microsoft.Extensions.Configuration;

namespace SecureExampleModule;

internal static class Endpoints
{
    internal static string ProtectedMessage(IConfiguration configuration)
    {
        return configuration.GetSection("SecureExampleModule:ProtectedMessage").Value;
    }

    internal static string? SettingsMessage(IConfiguration configuration)
    {
        return configuration.GetSection("SecureExampleModule:SampleMessage").Value;
    }
}