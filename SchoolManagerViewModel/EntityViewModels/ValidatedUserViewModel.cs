using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.Utils;
using SchoolManagerModel.Validators;

namespace SchoolManagerViewModel.EntityViewModels;

public class ValidatedUserViewModel : ViewModelBase
{
    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _confirmPassword = string.Empty;
    private string _email = string.Empty;
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    private string _usernameValidatorErrors = string.Empty;
    private string _emailValidatorErrors = string.Empty;
    private string _passwordValidatorErrors = string.Empty;
    private string _confirmPasswordValidatorErrors = string.Empty;
    private string _firstNameValidatorErrors = string.Empty;
    private string _lastNameValidatorErrors = string.Empty;

    public string Username
    {
        get => _username;
        set
        {
            SetField(ref _username, value, nameof(Username));
            ValidateUsernameAsync(_username);
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            SetField(ref _password, value, nameof(Password));
            ValidatePassword();
            ValidateConfirmPassword();
        }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            SetField(ref _confirmPassword, value, nameof(ConfirmPassword));
            ValidateConfirmPassword();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            SetField(ref _email, value, nameof(Email));
            ValidateEmailAsync(_email);
        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            SetField(ref _firstName, value, nameof(FirstName));
            ValidateFirstName();
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            SetField(ref _lastName, value, nameof(LastName));
            ValidateLastName();
        }
    }

    public string UsernameValidatorErrors
    {
        get => _usernameValidatorErrors;
        private set => SetField(ref _usernameValidatorErrors, value, nameof(UsernameValidatorErrors));
    }

    public string EmailValidatorErrors
    {
        get => _emailValidatorErrors;
        private set => SetField(ref _emailValidatorErrors, value, nameof(EmailValidatorErrors));
    }

    public string PasswordValidatorErrors
    {
        get => _passwordValidatorErrors;
        private set => SetField(ref _passwordValidatorErrors, value, nameof(PasswordValidatorErrors));
    }

    public string ConfirmPasswordValidatorErrors
    {
        get => _confirmPasswordValidatorErrors;
        private set => SetField(ref _confirmPasswordValidatorErrors, value, nameof(ConfirmPasswordValidatorErrors));
    }

    public string FirstNameValidatorErrors
    {
        get => _firstNameValidatorErrors;
        private set => SetField(ref _firstNameValidatorErrors, value, nameof(FirstNameValidatorErrors));
    }

    public string LastNameValidatorErrors
    {
        get => _lastNameValidatorErrors;
        private set => SetField(ref _lastNameValidatorErrors, value, nameof(LastNameValidatorErrors));
    }

    private async void ValidateUsernameAsync(string username)
    {
        // Original validation logic remains intact
        try
        {
            using var dbContext = new SchoolDbContext();
            var userDatabase = new UserDatabase(dbContext);
            var userManager = new UserManager(userDatabase);
            var validator = new ValidNotExistsUserValidator(userManager, UIResourceFactory.GetNewResource());
            var validationResult = await validator.ValidateAsync(new User { Username = username });
            UsernameValidatorErrors = ValidationErrors.GetErrorsFormatted(validationResult);
        }
        catch (Exception ex)
        {
            UsernameValidatorErrors = ex.Message;
        }
    }

    private async void ValidateEmailAsync(string email)
    {
        // Original validation logic remains intact
        try
        {
            using var dbContext = new SchoolDbContext();
            var userDatabase = new UserDatabase(dbContext);
            var userManager = new UserManager(userDatabase);
            var validator = new ValidNotExistsEmailValidator(userManager, UIResourceFactory.GetNewResource());
            var validationResult = await validator.ValidateAsync(new User { Email = email });
            EmailValidatorErrors = ValidationErrors.GetErrorsFormatted(validationResult);
        }
        catch (Exception ex)
        {
            EmailValidatorErrors = ex.Message;
        }
    }

    private void ValidatePassword()
    {
        var resourceManager = UIResourceFactory.GetNewResource();
        var validator = new PasswordValidator(resourceManager).Validate(Password);
        PasswordValidatorErrors = ValidationErrors.GetErrorsFormatted(validator);
    }

    private void ValidateConfirmPassword()
    {
        var resourceManager = UIResourceFactory.GetNewResource();
        var validator = new ConfirmPasswordValidator(resourceManager).Validate((Password, ConfirmPassword));
        ConfirmPasswordValidatorErrors = ValidationErrors.GetErrorsFormatted(validator);
    }

    private void ValidateFirstName()
    {
        var validator = new NotEmptyValidator(UIResourceFactory.GetNewResource());
        FirstNameValidatorErrors = ValidationErrors.GetErrorsFormatted(validator.Validate(FirstName));
    }

    private void ValidateLastName()
    {
        var validator = new NotEmptyValidator(UIResourceFactory.GetNewResource());
        LastNameValidatorErrors = ValidationErrors.GetErrorsFormatted(validator.Validate(LastName));
    }
}
