using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pro.Modular.Shared.Interfaces;
using ValidationModule.Endpoints;
using ValidationModule.Models;
using ValidationModule.Services;

namespace ValidationModule;

public class Module : IModule
{
    public string ModuleName => "ValidationModule";
    public string SettingsFileName => string.Empty;

    public WebApplicationBuilder RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductService, ProductService>();

        return builder;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var weatherForecasts = endpoints.MapGroup("/validation")
            .WithTags("Weather Forecasts")
            .WithOpenApi();

        weatherForecasts.MapGet("/weatherforecasts",
                async Task<Results<BadRequest, Ok<IEnumerable<Product>>>>
                    (IProductService weatherForecastService,
                        ILogger<ProductService> logger)
                    => await GetProducts.Forecast(weatherForecastService, logger))
            .Produces(StatusCodes.Status200OK, typeof(IEnumerable<Product>))
            .ProducesProblem(StatusCodes.Status400BadRequest);


        weatherForecasts.MapGet("/message",
                (IConfiguration configuration)
                    => configuration.GetSection("WeatherForecast:SampleMessage").Value)
            .Produces(StatusCodes.Status200OK, typeof(string));

        return endpoints;
    }

    public void BindOptions(WebApplicationBuilder builder)
    {
    }
}