using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Pro.Modular.Shared.Interfaces;

public interface IModule
{
    string settingsFileName { get; }
    WebApplicationBuilder RegisterModule(WebApplicationBuilder builder);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}