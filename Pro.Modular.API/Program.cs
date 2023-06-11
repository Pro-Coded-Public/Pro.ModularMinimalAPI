using Serilog;
using WeatherForecastModule.ModuleRegistration;

var builder = WebApplication.CreateBuilder(args);

// Use Serilog
// If needed, Clear default providers
builder.Logging.ClearProviders();
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.WriteTo.Console().WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureWeatherForecastModule();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }