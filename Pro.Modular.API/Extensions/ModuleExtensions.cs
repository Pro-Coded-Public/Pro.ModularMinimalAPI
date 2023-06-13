using Microsoft.Extensions.FileProviders;
using Pro.Modular.Shared;
using Pro.Modular.Shared.Interfaces;

namespace Pro.Modular.API.Extensions;

internal static class ModuleExtensions
{
    internal static List<IModule> DiscoverdModules = new();

    internal static WebApplicationBuilder DiscoverModules(this WebApplicationBuilder builder)
    {
        DiscoverdModules = InterfaceScanner.DiscoverModules().ToList();

        return builder;
    }

    internal static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        foreach (var module in DiscoverdModules) module.RegisterModule(builder);

        return builder;
    }

    internal static WebApplication MapModuleEndpoints(this WebApplication app)
    {
        foreach (var module in DiscoverdModules) module.MapEndpoints(app);
        return app;
    }

    internal static List<string> GetModuleSettingsFiles(this WebApplicationBuilder builder)
    {
        var settingsFiles = new List<string>();
        foreach (var module in DiscoverdModules)
            if (module.settingsFileName is not null)
                settingsFiles.Add(module.settingsFileName);

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