using System.Resources;
using FluentValidation;

namespace SchoolManagerModel.Validators;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator(ResourceManager resourceManager)
    {
        RuleFor(email => email)
            .NotNull()
            .NotEmpty()
            .WithMessage(resourceManager.GetString("MustNotBeEmpty"))
            .EmailAddress()
            .WithMessage(resourceManager.GetString("MustBeEmailAddress"));
    }
}