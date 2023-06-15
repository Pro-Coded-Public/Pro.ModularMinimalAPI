using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pro.Modular.Shared.Interfaces;

namespace ExampleModule;

public class Module : IModule
{
    public WebApplicationBuilder RegisterModule(WebApplicationBuilder builder)
    {
        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var examples = endpoints.MapGroup("/Examples")
            .WithTags("Examples")
            .WithOpenApi();

        examples.MapGet("/throw/{statusCode?}", Endpoints.ReturnException)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        examples.MapGet("/message", Endpoints.SettingsMessage)
            .Produces(StatusCodes.Status200OK, typeof(string));

        return endpoints;
    }

    public void BindOptions(WebApplicationBuilder builder)
    {
    }

    public string settingsFileName => @"../ExampleModule/exampleAppSettings.json";
}