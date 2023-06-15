using Microsoft.Extensions.Options;

namespace WeatherForecastModule.Services;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
}

public class WeatherForecastService : IWeatherForecastService
{

    private readonly IOptionsSnapshot<ForecastOptions> _forecastOptions;
    private static readonly string[] Summaries =
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    };
    
    public WeatherForecastService(IOptionsSnapshot<ForecastOptions> forecastOptions)
    {
        _forecastOptions = forecastOptions;
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
    {
        
        // check the options for number of results to generate
        return await Task.FromResult(Enumerable.Range(1, _forecastOptions.Value.ForecastCount)
            .Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray()
        );
    }
}