using ExampleModule.Extensions;
using Microsoft.Extensions.Options;
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

var app = builder.Build();

app.UseMiddleware();

app.UseExampleModuleEndPoints();
app.UseWeatherForecastEndPoints();

app.Run();

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program { }
