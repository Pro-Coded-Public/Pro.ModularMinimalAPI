using Microsoft.AspNetCore.Builder;
using WeatherForecastModule.Endpoints;

namespace WeatherForecastModule.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseWeatherForecastEndPoints(this WebApplication app)
    {
        WeatherForecastEndpoints.RegisterEndPoints(app);

        return app;
    }
}