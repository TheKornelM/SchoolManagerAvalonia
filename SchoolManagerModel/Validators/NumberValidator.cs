using FluentValidation;
using System.Resources;

namespace SchoolManagerModel.Validators;

public class NumberValidator : AbstractValidator<string>
{
    public NumberValidator(ResourceManager resourceManager)
    {
        RuleFor(a => a)
            .Must(x => int.TryParse(x, out var val))
            .WithMessage(resourceManager.GetString("MustBeNumber"));
    }
}