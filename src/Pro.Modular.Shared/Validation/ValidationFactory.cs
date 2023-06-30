using System.Net;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Pro.Modular.Shared.Validation;

public static class ValidationFilter
{
    public static EndpointFilterDelegate ValidationFilterFactory(EndpointFilterFactoryContext context,
        EndpointFilterDelegate next)
    {
        var validationDescriptors = GetValidators(context.MethodInfo, context.ApplicationServices);

        if (!validationDescriptors.Any()) return next;

        return invocationContext => Validate(validationDescriptors, invocationContext, next);
    }

    private static async ValueTask<object?> Validate(IEnumerable<ValidationDescriptor> validationDescriptors,
        EndpointFilterInvocationContext invocationContext, EndpointFilterDelegate next)
    {
        foreach (var descriptor in validationDescriptors)
        {
            var argument = invocationContext.Arguments[descriptor.ArgumentIndex];

            if (argument is null) continue;

            var validationResult = await descriptor.Validator.ValidateAsync(
                new ValidationContext<object>(argument)
            );

            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary(),
                    statusCode: (int)HttpStatusCode.UnprocessableEntity);
        }

        return await next.Invoke(invocationContext);
    }

    private static IEnumerable<ValidationDescriptor> GetValidators(MethodInfo methodInfo,
        IServiceProvider serviceProvider)
    {
        var parameters = methodInfo.GetParameters();

        for (var i = 0; i < parameters.Length; i++)
        {
            var parameter = parameters[i];

            if (parameter.GetCustomAttribute<ValidateAttribute>() is null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(parameter.ParameterType);

            // Note that FluentValidation validators need to be registered as singleton

            if (serviceProvider.GetService(validatorType) is IValidator validator)
                yield return new ValidationDescriptor
                    { ArgumentIndex = i, ArgumentType = parameter.ParameterType, Validator = validator };
        }
    }

    private class ValidationDescriptor
    {
        public required int ArgumentIndex { get; init; }
        public required Type ArgumentType { get; init; }
        public required IValidator Validator { get; init; }
    }
}