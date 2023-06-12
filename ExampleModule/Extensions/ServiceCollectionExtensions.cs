using ExampleModule.EndPoints;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleModule.Extensions;

public static class ServiceCollectionExtensions
{
    internal static void RegisterControllersAndServices(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(typeof(ExampleEndpoints).Assembly);
    }
}