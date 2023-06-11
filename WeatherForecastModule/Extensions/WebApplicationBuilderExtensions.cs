using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace WeatherForecastModule.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureWeatherForecastModule(this WebApplicationBuilder builder)
    {
        builder.ConfigureWeatherForecastModuleSettings();
        builder.Services.RegisterControllersAndServices();

        return builder;
    }

    private static WebApplicationBuilder ConfigureWeatherForecastModuleSettings(this WebApplicationBuilder builder)
    {
        var assemblyPath = typeof(WebApplicationBuilderExtensions).Assembly.Location;
        var directory = Path.GetDirectoryName(assemblyPath);
        var fileProvider = new PhysicalFileProvider(directory ?? throw new InvalidOperationException());
        builder.Services.AddSingleton<IFileProvider>(fileProvider);
        builder.Configuration.AddJsonFile(fileProvider, "weathermodulesettings.json", false, false);

        return builder;
    }
}