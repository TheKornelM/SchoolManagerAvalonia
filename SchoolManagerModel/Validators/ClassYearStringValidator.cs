using FluentValidation;
using System.Resources;

namespace SchoolManagerModel.Validators;

public class ClassYearStringValidator : AbstractValidator<string>
{
    public ClassYearStringValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x)
            .SetValidator(new NumberValidator(resourceManager))
            .DependentRules(() =>
            {
                // Check only if it is number
                RuleFor(x => x)
                    .Must(x => int.TryParse(x, out var num) && num > 0)
                    .When(x => int.TryParse(x, out var _))
                    .WithMessage(resourceManager.GetString("MustBeGreaterThan0"));

            });
    }
}