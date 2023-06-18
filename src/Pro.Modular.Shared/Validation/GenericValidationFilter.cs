using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Pro.Modular.Shared.Validation;

public class GenericValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public GenericValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T)) is not T obj)
            return TypedResults.BadRequest($"Unable to locate {typeof(T)} in request to validate ");

        var validationResult = await _validator.ValidateAsync(obj);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary(),
                "Please correct the following errors and try again",
                null,
                "Validation failed");

        return await next(context);
    }
}