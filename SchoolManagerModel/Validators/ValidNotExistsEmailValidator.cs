using System.Resources;
using FluentValidation;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;

namespace SchoolManagerModel.Validators;

public class ValidNotExistsEmailValidator : AbstractValidator<User>
{
    public ValidNotExistsEmailValidator(UserManager userManager, ResourceManager resourceManager)
    {
        RuleFor(x => x.Email)
            .SetValidator(new EmailValidator(resourceManager));
        RuleFor(x => x)
            .MustAsync(async (user, cancellation)
                => await new EmailNotRegisteredValidator(userManager).Validate(user, cancellation))
            .WithMessage(resourceManager.GetString("EmailAddressAlreadyRegistered"));
    }
}