using FluentValidation;

namespace ValidationModule.Models;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
        RuleFor(p => p.Category).NotEmpty().MaximumLength(50);
    }
}