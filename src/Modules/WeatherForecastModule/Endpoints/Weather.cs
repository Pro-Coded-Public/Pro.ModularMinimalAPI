using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using WeatherForecastModule.Services;

namespace WeatherForecastModule.Endpoints;

internal static class Weather
{
    internal static async Task<Results<BadRequest, Ok<IEnumerable<WeatherForecast>>>> Forecast(
        IWeatherForecastService weatherForecastService, ILogger<WeatherForecastService> logger)
    {
        var results = await weatherForecastService.GetWeatherForecasts();
        if (results is not null)
            return TypedResults.Ok(results);

        logger.LogInformation("No results found");
        return TypedResults.BadRequest();
    }
}