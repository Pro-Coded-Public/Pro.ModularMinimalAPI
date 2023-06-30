using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Pro.Modular.Shared.Interfaces;

public interface IModule
{
    string ModuleName { get; }

    string SettingsFileName { get; }

    // FileLocation settingsFile { get; }
    WebApplicationBuilder RegisterModule(WebApplicationBuilder builder);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);

    void BindOptions(WebApplicationBuilder builder);
}