using Pro.Modular.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

//https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6#conclusion

builder.DiscoverModules();
builder.Host.ConfigureAppSettings();
builder.AddServices();
builder.RegisterModules();


var app = builder.Build();

app.UseMiddleware();
app.MapModuleEndpoints();

app.Run();
