using FluentValidation;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;

namespace SchoolManagerModel.Validators;

public class UserNotExistsValidator : AbstractValidator<User>
{
    private readonly UserManager _userManager;

    public UserNotExistsValidator(UserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Validate(User user, CancellationToken cancellationToken)
    {
        return !await _userManager.UserExistsAsync(user);
    }
}