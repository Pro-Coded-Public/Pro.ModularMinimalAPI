using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace ExampleModule.EndPoints;

internal static class ExampleEndpoints
{
    internal static void RegisterEndPoints(WebApplication app)
    {
        var examples = app.MapGroup("/")
            .WithTags("Examples")
            .WithOpenApi();

        examples.MapGet("/throw/{statusCode?}", (int? statusCode) =>
            {
                throw statusCode switch
                {
                    >= 400 and < 500 => new BadHttpRequestException(
                        $"{statusCode} {ReasonPhrases.GetReasonPhrase(statusCode.Value)}",
                        statusCode.Value),
                    null => new Exception("uh oh"),
                    _ => new Exception($"Status code {statusCode}")
                };
            })
            .ProducesProblem(StatusCodes.Status400BadRequest);
        ;
    }
}