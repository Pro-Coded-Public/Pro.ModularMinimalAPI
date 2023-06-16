using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using ValidationModule.Models;
using ValidationModule.Services;

namespace ValidationModule.Endpoints;

internal static class GetProducts
{
    internal static async Task<Results<BadRequest, Ok<IEnumerable<Product>>>> Forecast(
        IProductService productService, ILogger<ProductService> logger)
    {
        var results = await productService.GetProducts();
        if (results is not null)
            return TypedResults.Ok(results);

        logger.LogInformation("No results found");
        return TypedResults.BadRequest();
    }
}