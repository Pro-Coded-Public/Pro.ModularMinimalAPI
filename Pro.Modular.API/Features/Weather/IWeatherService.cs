using Pro.Modular.API.Features.Weather.Models;

namespace Pro.Modular.API.Features.Weather;
public interface IWeatherService
{
    WeatherForecast[] GetWeather();
}