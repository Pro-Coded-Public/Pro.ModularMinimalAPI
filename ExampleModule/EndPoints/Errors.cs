using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace ExampleModule.EndPoints;

internal static class Errors
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
}