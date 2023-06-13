using ExampleModule.EndPoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Pro.Modular.Shared.Interfaces;

namespace ExampleModule;

public class Module : IModule
{
    public WebApplicationBuilder RegisterModule(WebApplicationBuilder builder)
    {
        // builder.ConfigureModuleSettings<Module>(settingsFileName);

        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var examples = endpoints.MapGroup("/Examples")
            .WithTags("Examples")
            .WithOpenApi();

        examples.MapGet("/throw/{statusCode?}", Errors.ReturnException)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        examples.MapGet("/message", (IConfiguration configuration) =>
                configuration.GetSection("ExampleModule:SampleMessage").Value)
            .Produces(StatusCodes.Status200OK, typeof(string));

        return endpoints;
    }


    public string settingsFileName => @"../ExampleModule\exampleAppSettings.json";
}