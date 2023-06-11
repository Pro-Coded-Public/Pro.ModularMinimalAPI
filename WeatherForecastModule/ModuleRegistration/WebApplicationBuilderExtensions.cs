using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using WeatherForecastModule.Controllers;

namespace WeatherForecastModule.ModuleRegistration;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureWeatherForecastModule(this WebApplicationBuilder builder)
    {
        RegisterControllers(builder.Services);
        ConfigureWeatherForecastModuleSettings(builder);
        LogSampleMessage(builder);
        return builder;
    }

    private static void LogSampleMessage(WebApplicationBuilder builder)
    {
        var message = builder.Configuration.GetValue<string>("SampleMessage");
    }

    private static void ConfigureWeatherForecastModuleSettings(WebApplicationBuilder builder)
    {
        var assemblyPath = typeof(WebApplicationBuilderExtensions).Assembly.Location;
        var directory = Path.GetDirectoryName(assemblyPath);
        var fileProvider = new PhysicalFileProvider(directory);
        builder.Services.AddSingleton<IFileProvider>(fileProvider);
        builder.Configuration.AddJsonFile(fileProvider, "weathermodulesettings.json", false, false);
    }
    public static void RegisterControllers(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(typeof(WeatherForecastController).Assembly);

        services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
    }
}