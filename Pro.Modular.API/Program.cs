using Pro.Modular.API.Extensions;
using WeatherForecastModule.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddAndConfigureSerilog();
builder.ConfigureWeatherForecastModule();
builder.AddAndConfigureSwagger();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(exceptionHandlerApp
        => exceptionHandlerApp.Run(async context => await Results.Problem().ExecuteAsync(context)));
    app.UseStatusCodePages();
}

app.UseHttpsRedirection();

app.ConfigureApiRoutes();

app.Run();

// Make the implicit Program class public so test projects can access it
namespace Pro.Modular.API
{
    public class Program
    {
    }
}