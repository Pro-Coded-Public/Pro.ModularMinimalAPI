using Pro.Modular.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.DiscoverModules();
builder.Host.ConfigureAppSettings();
builder.BindOptions();
builder.AddServices();
builder.AddModuleServices();

var app = builder.Build();

app.UseMiddleware();
app.UseModuleMiddleware();
app.MapModuleEndpoints();

app.Run();