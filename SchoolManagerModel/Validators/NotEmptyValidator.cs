using System.Resources;
using FluentValidation;

namespace SchoolManagerModel.Validators;

public class NotEmptyValidator : AbstractValidator<string>
{
    public NotEmptyValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x)
            .NotNull()
            .NotEmpty()
            .WithMessage(resourceManager.GetString("MustNotBeEmpty"));
    }
}