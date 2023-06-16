using Microsoft.OpenApi.Models;
using Pro.Modular.Shared.Models;

namespace Pro.Modular.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    internal static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.AddAndConfigureSerilog();
        builder.AddAndConfigureSwagger();
        builder.Services.AddProblemDetails();
        builder.Services.AddCors();
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        return builder;
    }

    private static WebApplicationBuilder AddAndConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((hostContext, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(hostContext.Configuration);
        });

        return builder;
    }

    private static WebApplicationBuilder AddAndConfigureSwagger(this WebApplicationBuilder builder)
    {
        var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
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
                        Name = "Example License"
                    }
                });

            options.AddSecurityDefinition("oauth2",
                new OpenApiSecurityScheme
                {
                    Description = "OAuth2.0 Auth Code with PKCE",
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(jwtOptions.AuthorizationUrl),
                            TokenUrl = new Uri(jwtOptions.TokenUrl),
                            Scopes = new Dictionary<string, string>
                            {
                                { jwtOptions.ApiScope, "read the api" }
                            }
                        }
                    }
                });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    new[] { jwtOptions.ApiScope }
                }
            });

            options.AddSecurityDefinition("ApiKey",
                new OpenApiSecurityScheme
                {
                    Description = "The Api Key to access the API",
                    Name = "X-API-Key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKeyScheme"
                });

            var apiKeyScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" },
                In = ParameterLocation.Header
            };

            var apiKeyRequirement = new OpenApiSecurityRequirement
            {
                { apiKeyScheme, new List<string>() }
            };

            options.AddSecurityRequirement(apiKeyRequirement);
        });

        builder.Services.AddEndpointsApiExplorer();

        return builder;
    }
}