using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using WeatherForecastModule.Services;

namespace WeatherForecastModule.Endpoints;

internal static class WeatherForecastEndpoints
{
    internal static void RegisterEndPoints(WebApplication app)
    {
        var weatherForecasts = app.MapGroup("/")
            .WithTags("Weather Forecasts")
            .WithOpenApi();

        weatherForecasts.MapGet("/weatherforecasts", async Task<Results<BadRequest, Ok<IEnumerable<WeatherForecast>>>>
                (IWeatherForecastService weatherForecastService, ILogger<WeatherForecastService> logger) =>
            {
                var results = await weatherForecastService.GetWeatherForecasts();
                if (results is not null)
                    return TypedResults.Ok(results);

                logger.LogInformation("No results found");
                return TypedResults.BadRequest();
            })
            .Produces(StatusCodes.Status200OK, typeof(IEnumerable<WeatherForecast>))
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}