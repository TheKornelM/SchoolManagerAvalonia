using FluentValidation;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;

namespace SchoolManagerModel.Validators;

public class EmailNotRegisteredValidator(UserManager userManager) : AbstractValidator<User>
{
    public async Task<bool> Validate(User user, CancellationToken cancellationToken)
    {
        return !await userManager.EmailAlreadyRegisteredAsync(user.Email);
    }
}