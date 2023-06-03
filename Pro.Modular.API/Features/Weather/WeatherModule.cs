namespace Pro.Modular.API.Features.Weather;

public class WeatherModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection builder)
    {
        _ = builder.AddScoped<IWeatherService, WeatherService>();

        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        _ = endpoints.MapGet("/weatherforecast", (IWeatherService weatherService) => weatherService.GetWeather())
                .WithName("GetWeatherForecast");

        return endpoints;
    }
}