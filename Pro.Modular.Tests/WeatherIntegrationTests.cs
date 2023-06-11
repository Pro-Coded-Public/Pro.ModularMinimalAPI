using System.Net.Http.Json;

namespace Pro.Modular.Tests;

public class WeatherIntegrationTests
{
    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        var api = new ApiWebApplicationFactory();
        var forecast = await api.CreateClient().GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");
        forecast.Should().HaveCount(5);
    }
}