namespace Pro.Modular.API.Features.Weather.Models;

public record WeatherForecast(int TemperatureC)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}