using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pro.Modular.Shared.Filters;
using Pro.Modular.Shared.Interfaces;

namespace SecureExampleModule;

public class Module : IModule
{
    public string ModuleName => "SecureExampleModule";

    public string SettingsFileName => "secureExampleAppSettings.json";

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
//            .RequireAuthorization("ApiKeyPolicy");


        secureExamples.MapGet("/apikeyprotectedmessage", Endpoints.ProtectedMessage)
            .WithDescription("Api Key Protected")
            .Produces(StatusCodes.Status200OK, typeof(string))
            .AddEndpointFilter<ApiKeyEndpointFilterAsync>();

        secureExamples.MapGet("/ouathprotectedmessage", Endpoints.ProtectedMessage)
            .WithDescription("OAuth Protected")
            .Produces(StatusCodes.Status200OK, typeof(string))
            .RequireAuthorization();

        return endpoints;
    }

    public void BindOptions(WebApplicationBuilder builder)
    {
    }
}