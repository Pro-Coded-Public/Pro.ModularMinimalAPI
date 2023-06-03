using Pro.Modular.API.Features.Weather.Models;

namespace Pro.Modular.API.Features.Weather;

public class WeatherService : IWeatherService
{

    public WeatherForecast[] GetWeather() => Enumerable.Range(1, 5).Select(_ => new WeatherForecast
    (Random.Shared.Next(-20, 55)
    )).ToArray();
}