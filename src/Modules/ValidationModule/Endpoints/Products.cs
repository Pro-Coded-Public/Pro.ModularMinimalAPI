using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using ValidationModule.Models;
using ValidationModule.Services;

namespace ValidationModule.Endpoints;

internal static class Products
{
    internal static async Task<Results<BadRequest, Ok<IEnumerable<Product>>>> GetProducts(
        IProductService productService, ILogger<ProductService> logger)
    {
        var results = await productService.GetProducts();
        if (results is not null)
            return TypedResults.Ok(results);

        logger.LogInformation("No results found");
        return TypedResults.BadRequest();
    }

    public static async Task<Product> CreateProduct(IProductService productService, ILogger<ProductService> logger,
        Product product)
    {
        return await productService.CreateProduct(product);
    }
}