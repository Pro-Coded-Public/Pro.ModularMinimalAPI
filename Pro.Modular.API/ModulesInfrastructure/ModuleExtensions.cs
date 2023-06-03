﻿namespace Pro.Modular.API.ModulesInfrastructure;

public static class ModuleExtensions
{
    // this could also be added into the DI container
    private static readonly List<IModule> RegisteredModules = new();

    public static IServiceCollection RegisterModules(this IServiceCollection services)
    {
        var modules = DiscoverModules();
        foreach (var module in modules)
        {
            _ = module.RegisterModule(services);
            RegisteredModules.Add(module);
        }

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        foreach (var module in RegisteredModules)
        {
            _ = module.MapEndpoints(app);
        }
        return app;
    }

    private static IEnumerable<IModule> DiscoverModules() => typeof(IModule).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
}