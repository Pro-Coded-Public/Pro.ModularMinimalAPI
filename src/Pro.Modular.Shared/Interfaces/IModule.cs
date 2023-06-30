using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Pro.Modular.Shared.Interfaces;

public interface IModule
{
    string ModuleName { get; }

    string SettingsFileName { get; }

    WebApplicationBuilder AddModuleServices(WebApplicationBuilder builder);
    WebApplication UseModuleMiddleware(WebApplication app);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    void BindOptions(WebApplicationBuilder builder);
}