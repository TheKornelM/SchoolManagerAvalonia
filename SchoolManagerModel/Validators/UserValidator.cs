using System.Resources;
using FluentValidation;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator(ResourceManager resourceManager)
    {
        RuleFor(user => user.Username).SetValidator(new NotEmptyValidator(resourceManager));
        RuleFor(user => user.Email).SetValidator(new EmailValidator(resourceManager));
        RuleFor(user => user.Password).SetValidator(new PasswordValidator(resourceManager));
        RuleFor(user => user.FirstName).SetValidator(new NotEmptyValidator(resourceManager));
        RuleFor(user => user.LastName).SetValidator(new NotEmptyValidator(resourceManager));
    }
}