﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pro.Modular.Shared.Interfaces;
using WeatherForecastModule.Endpoints;
using WeatherForecastModule.Services;

namespace WeatherForecastModule;

public class Module : IModule
{
    public string ModuleName => "WeatherForecastModule";
    public string SettingsFileName => "weatherForecastAppSettings.json";

    public WebApplicationBuilder AddModuleServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

        return builder;
    }

    public WebApplication UseModuleMiddleware(WebApplication app)
    {
        return app;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var weatherForecasts = endpoints.MapGroup("/weather")
            .WithTags("Weather Forecasts")
            .WithOpenApi();

        weatherForecasts.MapGet("/weatherforecasts",
                async Task<Results<BadRequest, Ok<IEnumerable<WeatherForecast>>>>
                    (IWeatherForecastService weatherForecastService,
                        ILogger<WeatherForecastService> logger)
                    => await Weather.Forecast(weatherForecastService, logger))
            .Produces(StatusCodes.Status200OK, typeof(IEnumerable<WeatherForecast>))
            .ProducesProblem(StatusCodes.Status400BadRequest);


        weatherForecasts.MapGet("/message",
                (IConfiguration configuration)
                    => configuration.GetSection("WeatherForecast:SampleMessage").Value)
            .Produces(StatusCodes.Status200OK, typeof(string));

        return endpoints;
    }

    public void BindOptions(WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<ForecastOptions>()
            .BindConfiguration("WeatherForecast")
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}