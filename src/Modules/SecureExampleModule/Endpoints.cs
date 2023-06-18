using Microsoft.Extensions.Configuration;

namespace SecureExampleModule;

internal static class Endpoints
{
    private const string secureexamplemoduleSamplemessage = "SecureExampleModule:SampleMessage";

    internal static string ProtectedMessage(IConfiguration configuration)
    {
        return configuration.GetSection("SecureExampleModule:ProtectedMessage").Value;
    }
}