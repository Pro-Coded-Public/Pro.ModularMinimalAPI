using Pro.Modular.Shared;
using Pro.Modular.Shared.Interfaces;

namespace Pro.Modular.API.Extensions;

internal static class ModuleExtensions
{
    internal static List<IModule> DiscoveredModules = new();

    internal static WebApplicationBuilder DiscoverModules(this WebApplicationBuilder builder)
    {
        DiscoveredModules = InterfaceScanner.DiscoverModules().ToList();

        return builder;
    }

    internal static WebApplicationBuilder AddModuleServices(this WebApplicationBuilder builder)
    {
        foreach (var module in DiscoveredModules) module.AddModuleServices(builder);

        return builder;
    }

    internal static WebApplication UseModuleMiddleware(this WebApplication app)
    {
        foreach (var module in DiscoveredModules) module.UseModuleMiddleware(app);

        return app;
    }

    internal static WebApplicationBuilder BindOptions(this WebApplicationBuilder builder)
    {
        foreach (var module in DiscoveredModules) module.BindOptions(builder);
        return builder;
    }

    internal static WebApplication MapModuleEndpoints(this WebApplication app)
    {
        foreach (var module in DiscoveredModules) module.MapEndpoints(app);
        return app;
    }
}