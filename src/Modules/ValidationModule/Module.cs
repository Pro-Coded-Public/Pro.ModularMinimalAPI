using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Pro.Modular.Shared.Interfaces;
using Pro.Modular.Shared.Validation;
using ValidationModule.Endpoints;
using ValidationModule.Models;
using ValidationModule.Services;

namespace ValidationModule;

public class Module : IModule
{
    public string ModuleName => "ValidationModule";
    public string SettingsFileName => string.Empty;

    public WebApplicationBuilder AddModuleServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddSingleton<IValidator<Product>, ProductValidator>();

        return builder;
    }

    public WebApplication UseModuleMiddleware(WebApplication app)
    {
        return app;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var products = endpoints.MapGroup("/product")
            .WithTags("Validation")
            .WithOpenApi();
        // .AddEndpointFilter<ProductValidationFilter>(); Optionally add the filter here
        // .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory); Optionally add the filter factory here

        // Option 1 is to use a simple endpoint filter
        products.MapPost("/standardfilter",
                async Task<Results<BadRequest, Ok<Product>>>
                    (IProductService productService,
                        ILogger<ProductService> logger, Product product)
                    =>
                {
                    var createdProduct = await Products.CreateProduct(productService, logger, product);
                    if (createdProduct is not null)
                        return TypedResults.Ok(createdProduct);

                    logger.LogInformation("No results found");
                    return TypedResults.BadRequest();
                })
            .AddEndpointFilter<ProductValidationFilter>()
            .Produces(StatusCodes.Status200OK, typeof(IEnumerable<Product>))
            .ProducesProblem(StatusCodes.Status400BadRequest);

        // Option 2 is to use a generic endpoint filter
        products.MapPost("/genericfilter",
                async Task<Results<BadRequest, Ok<Product>>>
                    (IProductService productService,
                        ILogger<ProductService> logger, Product product)
                    =>
                {
                    var createdProduct = await Products.CreateProduct(productService, logger, product);
                    if (createdProduct is not null)
                        return TypedResults.Ok(createdProduct);

                    logger.LogInformation("No results found");
                    return TypedResults.BadRequest();
                })
            .AddEndpointFilter<GenericValidationFilter<Product>>()
            .Produces(StatusCodes.Status200OK, typeof(IEnumerable<Product>))
            .ProducesProblem(StatusCodes.Status400BadRequest);

        // Option 3 is to use filter factory
        products.MapPost("/filterfactory",
                async Task<Results<BadRequest, Ok<Product>>>
                    (IProductService productService,
                        ILogger<ProductService> logger, [Validate] Product product)
                    =>
                {
                    var createdProduct = await Products.CreateProduct(productService, logger, product);
                    if (createdProduct is not null)
                        return TypedResults.Ok(createdProduct);

                    logger.LogInformation("No results found");
                    return TypedResults.BadRequest();
                })
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .Produces(StatusCodes.Status200OK, typeof(IEnumerable<Product>))
            .ProducesProblem(StatusCodes.Status400BadRequest);


        return endpoints;
    }

    public void BindOptions(WebApplicationBuilder builder)
    {
    }
}