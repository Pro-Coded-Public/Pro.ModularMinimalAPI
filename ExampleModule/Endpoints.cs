using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace ExampleModule;

internal static class Endpoints
{
    internal static void ReturnException(int? statusCode)
    {
        throw statusCode switch
        {
            >= 400 and < 500 => new BadHttpRequestException(
                $"{statusCode} {ReasonPhrases.GetReasonPhrase(statusCode.Value)}",
                statusCode.Value),
            null => new Exception("uh oh"),
            _ => new Exception($"Status code {statusCode}")
        };
    }

    internal static string? SettingsMessage(IConfiguration configuration)
    {
        return configuration.GetSection("ExampleModule:SampleMessage").Value;
    }
}