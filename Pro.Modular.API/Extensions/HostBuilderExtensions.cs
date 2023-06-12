using Pro.Modular.Shared.Models;

namespace Pro.Modular.API.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureAppSettings(this IHostBuilder hostBuilder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var machineName = Environment.MachineName;

        hostBuilder.ConfigureAppConfiguration((ctx, builder) =>
        {
            builder.Sources.Clear();
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{environment}.json", true, true);
            builder.AddJsonFile($"appsettings.{machineName}.json", true, true);
            builder.AddEnvironmentVariables();
        });

        return hostBuilder;
    }

    private static void AddOptions(HostBuilderContext context, IServiceCollection serviceProvider)
    {
        var configuration = context.Configuration;

        // .Services.AddOptions<JwtOptions>()
        //     .Bind(builder.Configuration.GetSection(nameof(JwtOptions)))
        //     .ValidateDataAnnotations()

        serviceProvider.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection("JwtOptions"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        serviceProvider.AddOptions<Logging>()
            .Bind(configuration.GetSection("Logging"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}