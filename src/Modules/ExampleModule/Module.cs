using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pro.Modular.Shared.Interfaces;
using Pro.Modular.Shared.Models;

namespace ExampleModule;

public class Module : IModule
{
    public FileLocation settingsFile =>
        new()
        {
            FileName = "exampleAppSettings.json",
            Path = @"Modules/example/exampleAppSettings.json"
        };

    public string ModuleName => "ExampleModule";

    public string SettingsFileName => "exampleAppSettings.json";

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
}