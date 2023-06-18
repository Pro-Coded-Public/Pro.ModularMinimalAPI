using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace ValidationModule.Models;

public class ProductValidationFilter : IEndpointFilter
{
    private readonly IValidator<Product> _validator;

    public ProductValidationFilter(IValidator<Product> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        if (context.Arguments.FirstOrDefault(a => a.GetType() == typeof(Product)) is not Product product)
            return await next(context);

        var validationResult = await _validator.ValidateAsync(product);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary(),
                "Please correct the following errors and try again",
                null,
                "Validation failed");

        return await next(context);
    }
}