using System.Net.Http.Json;
using Pro.Modular.IntegrationTests.Helpers;

namespace Pro.Modular.IntegrationTests;

public class WeatherIntegrationTests
{
    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        var api = new ApiWebApplicationFactory();
        var forecast = await api.CreateClient().GetFromJsonAsync<WeatherForecast[]>("/weather/weatherforecasts");
        forecast.Should().HaveCount(5);
    }
}