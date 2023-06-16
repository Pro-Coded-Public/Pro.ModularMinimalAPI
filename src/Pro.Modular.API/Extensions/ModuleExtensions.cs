using Microsoft.Extensions.FileProviders;
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

    internal static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        foreach (var module in DiscoveredModules) module.RegisterModule(builder);

        return builder;
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

    internal static List<string> GetModuleSettingsFiles(this WebApplicationBuilder builder)
    {
        var settingsFiles = new List<string>();
        foreach (var module in DiscoveredModules)
            if (module.SettingsFileName is not null)
                settingsFiles.Add(module.SettingsFileName);

        return settingsFiles;
    }

    internal static void ConfigureModuleSettings<T>(this WebApplicationBuilder builder, string fileName)
    {
        var assembly = typeof(T).Assembly;
        var assemblyDirectory = Path.GetDirectoryName(assembly.Location);

        var fileProvider = new PhysicalFileProvider(assemblyDirectory);
        builder.Services.AddSingleton<IFileProvider>(fileProvider);

        builder.Configuration.AddJsonFile(fileProvider, fileName, false, false);
    }
}