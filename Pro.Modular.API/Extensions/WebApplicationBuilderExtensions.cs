using Microsoft.OpenApi.Models;

namespace Pro.Modular.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAndConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((hostContext, services, configuration) =>
        {
            configuration.WriteTo.Console().WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day);
        });
        return builder;
    }

    public static WebApplicationBuilder AddAndConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Sample modular API",
                Description = "An ASP.NET Core Web API",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        return builder;
    }
}