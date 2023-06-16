using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Pro.Modular.Shared.Filters;

public class ApiKeyEndpointFilterAsync : IEndpointFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyEndpointFilterAsync(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        if (context.HttpContext.Request.Headers.TryGetValue("X-API-Key", out var apiKey))
        {
            if (apiKey == _configuration["SecureExampleModule:ApiKey"]) return await next(context);

            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return new UnauthorizedResult();
        }

        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return new UnauthorizedResult();
    }
}

public class UnauthorizedResult : ProblemDetails
{
    public UnauthorizedResult()
    {
        Title = "Unauthorized";
        Status = StatusCodes.Status401Unauthorized;
        Detail = "Invalid API Key";
    }
}