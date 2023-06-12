using Microsoft.Extensions.DependencyInjection;
using WeatherForecastModule.Services;

namespace WeatherForecastModule.Extensions;

public static class ServiceCollectionExtensions
{
    internal static void RegisterControllersAndServices(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(typeof(WeatherForecast).Assembly);
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
    }
}