using ExampleModule.EndPoints;
using Microsoft.AspNetCore.Builder;

namespace ExampleModule.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureExampleModuleEndPoints(this WebApplication app)
    {
        ExampleEndpoints.RegisterEndPoints(app);

        return app;
    }
}