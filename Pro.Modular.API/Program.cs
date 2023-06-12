using ExampleModule.Extensions;
using Pro.Modular.API.Extensions;
using Pro.Modular.Shared.Models;
using WeatherForecastModule.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppSettings();
builder.AddAndConfigureSerilog();
builder.AddAndConfigureSwagger();
builder.Services.AddProblemDetails();

builder.AddExampleModule();
builder.AddWeatherForecastModule();

builder.Services.AddOptions<JwtOptions>()
    .BindConfiguration("JwtOptions")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.Configure<Logging>(
    builder.Configuration.GetRequiredSection("Logging"));

var settings = builder.Configuration.GetRequiredSection("JwtOptions").Get<JwtOptions>();

var app = builder.Build();

app.UseSwaggerEndpoint();
app.UseMiddleware();
app.UseErrorHandling();

app.UseExampleModuleEndPoints();
app.UseWeatherForecastEndPoints();

app.Run();

// Make the implicit Program class public so test projects can access it
namespace Pro.Modular.API
{
    public class Program
    {
    }
}