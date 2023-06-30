using Pro.Modular.Shared.Models;

namespace Pro.Modular.API.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureAppSettings(this IHostBuilder hostBuilder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var machineName = Environment.MachineName;

        hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            var modulesFolder = Path.Combine(context.HostingEnvironment.ContentRootPath, "..", "Modules");
            builder.Sources.Clear();
            foreach (var module in ModuleExtensions.DiscoveredModules)
            {
                if (string.IsNullOrWhiteSpace(module.SettingsFileName)) continue;

                var settingsFileAbsolutePath = Path.Combine(modulesFolder, module.ModuleName, module.SettingsFileName);
                if (!File.Exists(settingsFileAbsolutePath))
                    throw new Exception($"{module.SettingsFileName} not found at {settingsFileAbsolutePath}");

                builder.AddJsonFile(settingsFileAbsolutePath, false,
                    true);
            }

            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{environment}.json", true, true);
            builder.AddJsonFile($"appsettings.{machineName}.json", true, true);
            builder.AddEnvironmentVariables();
            context.Configuration.Bind(builder.Build());
        });

        hostBuilder.ConfigureServices(AddOptions);

        return hostBuilder;
    }

    private static void AddOptions(HostBuilderContext context, IServiceCollection serviceProvider)
    {
        serviceProvider.AddOptions<JwtOptions>()
            .BindConfiguration("JwtOptions")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        serviceProvider.AddOptions<Logging>()
            .BindConfiguration("Logging")
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}