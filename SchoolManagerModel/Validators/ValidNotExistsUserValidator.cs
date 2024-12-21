using System.Resources;
using FluentValidation;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;

namespace SchoolManagerModel.Validators;

public class ValidNotExistsUserValidator : AbstractValidator<User>
{
    public ValidNotExistsUserValidator(UserManager userManager, ResourceManager resourceManager)
    {
        RuleFor(x => x.Username)
            .SetValidator(new UsernameValidator(resourceManager));
        RuleFor(x => x)
            .MustAsync(async (user, cancellation)
                => await new UserNotExistsValidator(userManager).Validate(user, cancellation))
            .WithMessage(resourceManager.GetString("UserAlreadyExists"));
    }
}