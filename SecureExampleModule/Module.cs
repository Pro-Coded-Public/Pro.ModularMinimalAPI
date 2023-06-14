using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pro.Modular.Shared.Filters;
using Pro.Modular.Shared.Interfaces;

namespace SecureExampleModule;

public class Module : IModule
{
    public WebApplicationBuilder RegisterModule(WebApplicationBuilder builder)
    {
        // builder.ConfigureModuleSettings<Module>(settingsFileName);

        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var secureExamples = endpoints.MapGroup("/SecureExamples")
            .WithTags("Secure Examples")
            .WithOpenApi();

        secureExamples.MapGet("/protectedMessage", Endpoints.ProtectedMessage)
            .WithDescription("Api Key Protected")
            .Produces(StatusCodes.Status200OK, typeof(string))
            .AddEndpointFilter<ApiKeyEndpointFilterAsync>();

        secureExamples.MapGet("/message", Endpoints.SettingsMessage)
            .Produces(StatusCodes.Status200OK, typeof(string));

        return endpoints;
    }

    public string settingsFileName => @"../SecureExampleModule/secureExampleAppSettings.json";
}