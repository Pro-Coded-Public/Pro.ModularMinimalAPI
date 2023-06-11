using Microsoft.AspNetCore.Builder;
using WeatherForecastModule.Endpoints;

namespace WeatherForecastModule.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApiRoutes(this WebApplication app)
    {
        ExampleEndpoints.RegisterExampleEndPoints(app);
        WeatherForecastEndpoints.RegisterWeatherForecastEndPoints(app);

        return app;
    }
}