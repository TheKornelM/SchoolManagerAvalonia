using FluentValidation;
using System.Resources;

namespace SchoolManagerModel.Validators;

public class CharLetterValidator : AbstractValidator<string>
{
    public CharLetterValidator(ResourceManager resourceManager)
    {
        RuleFor(x => x)
            .Length(1)
            .WithMessage(resourceManager.GetString("CanContainOnly1Letter")) // Check the length first
            .DependentRules(() =>
            {
                RuleFor(x => x)
                    .Must(cls => cls.Length == 1 && Char.IsLetter(cls[0]))
                    .WithMessage(resourceManager.GetString("MustBeALetter"))
                    .When(cls => cls.Length == 1); // Ensure length is 1 before accessing the character
            });



    }
}