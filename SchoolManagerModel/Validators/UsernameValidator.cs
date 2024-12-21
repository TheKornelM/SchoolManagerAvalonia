using FluentValidation;
using System.Resources;

namespace SchoolManagerModel.Validators;

public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator(ResourceManager resourceManager)
    {
        RuleFor(username => username)
            .NotNull()
            .NotEmpty()
            .WithMessage(resourceManager.GetString("MustNotBeEmpty"))
            .Matches(@"^[a-zA-Z0-9]+$")
            .WithMessage(resourceManager.GetString("ShouldContainsOnlyLettersAndNumbers"));
    }
}