using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ExampleModule.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureExampleModule(this WebApplicationBuilder builder)
    {
        builder.ConfigureModuleSettings();
        builder.Services.RegisterControllersAndServices();

        return builder;
    }

    private static WebApplicationBuilder ConfigureModuleSettings(this WebApplicationBuilder builder)
    {
        var assemblyPath = typeof(WebApplicationBuilderExtensions).Assembly.Location;
        var directory = Path.GetDirectoryName(assemblyPath);
        var fileProvider = new PhysicalFileProvider(directory ?? throw new InvalidOperationException());
        builder.Services.AddSingleton<IFileProvider>(fileProvider);
        builder.Configuration.AddJsonFile(fileProvider, "examplemodulesettings.json", false, false);

        return builder;
    }
}