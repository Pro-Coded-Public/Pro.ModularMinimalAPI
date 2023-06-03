using System.Net.Http.Json;
using FluentAssertions;

using Pro.Modular.Tests.Helpers;

namespace Pro.Modular.Tests;

public class WeatherIntegrationTests
{
    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        var api = new ApiWebApplicationFactory();
        var forecast = await api.CreateClient().GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");
        _ = forecast.Should().HaveCount(5);
    }
}