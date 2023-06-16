using Microsoft.Extensions.Configuration;

namespace SecureExampleModule;

internal static class Endpoints
{
    const string secureexamplemoduleSamplemessage = "SecureExampleModule:SampleMessage";
    internal static string ProtectedMessage(IConfiguration configuration)
    {
        return configuration.GetSection("SecureExampleModule:ProtectedMessage").Value;
    }

    internal static string? SettingsMessage(IConfiguration configuration)
    {
        return configuration.GetValue<string>(secureexamplemoduleSamplemessage);
    }
}