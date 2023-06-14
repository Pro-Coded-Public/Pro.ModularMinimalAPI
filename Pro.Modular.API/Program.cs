using Pro.Modular.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.DiscoverModules();
builder.Host.ConfigureAppSettings();
builder.AddServices();
builder.RegisterModules();

var app = builder.Build();

app.UseMiddleware();
app.MapModuleEndpoints();

app.Run();