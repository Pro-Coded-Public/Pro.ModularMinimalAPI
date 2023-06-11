using Pro.Modular.API.Extensions;
using WeatherForecastModule.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddAndConfigureSerilog();
builder.ConfigureWeatherForecastModule();
builder.AddAndConfigureSwagger();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.ConfigureApiRoutes();
// app.MapControllers();

app.Run();

// Make the implicit Program class public so test projects can access it
namespace Pro.Modular.API
{
    public class Program
    {
    }
}