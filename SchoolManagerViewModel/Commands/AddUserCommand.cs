using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.Utils;
using SchoolManagerModel.Validators;
using System.Diagnostics;
using System.Windows.Input;

namespace SchoolManagerViewModel.Commands;

public class AddUserCommand : ICommand

{
    private AddUserViewModel _addUserViewModel;
    public event EventHandler? CanExecuteChanged;

    public AddUserCommand(AddUserViewModel viewModel)
    {
        _addUserViewModel = viewModel;
    }

    public bool CanExecute(object? parameter)
    {
        var userValidator = new UserValidator(UIResourceFactory.GetNewResource()).Validate(CreateUserFromViewModel());
        Debug.WriteLine(ValidationErrors.GetErrorsFormatted(userValidator));

        var role = _addUserViewModel.SelectedRole;
        bool studentHasClass = role != Role.Student || (role == Role.Student && _addUserViewModel.Class != null);

        return userValidator.IsValid && studentHasClass;
    }

    public async void Execute(object? parameter)
    {
        if (CanExecuteChanged == null)
        {
            return;
        }

        try
        {
            using var dbContext = new SchoolDbContext();
            var userDatabase = new UserDatabase(dbContext);
            var userManager = new UserManager(userDatabase);

            var user = CreateUserFromViewModel();

            await userManager.RegisterUserAsync(user);
            await userManager.AssignRoleAsync(user, _addUserViewModel.SelectedRole);

            switch (_addUserViewModel.SelectedRole)
            {
                case Role.Student:
                    if (_addUserViewModel.Class == null)
                    {
                        throw new NullReferenceException();
                    }

                    var student = new Student() { User = user, Class = _addUserViewModel.Class };
                    await userManager.AddStudentAsync(student);

                    var subjectDatabase = new SubjectDatabase(dbContext);
                    var subjectManager = new SubjectManager(subjectDatabase);
                    var subjects = _addUserViewModel.Subjects
                        .Where(x => x.IsChecked)
                        .Select(x => x.Item)
                        .ToList();
                    await subjectManager.AssignSubjectsToStudentAsync(student, subjects);

                    break;
                case Role.Teacher:
                    await userManager.AddTeacherAsync(new Teacher() { User = user });
                    break;
                case Role.Administrator:
                    await userManager.AddAdminAsync(new Admin() { User = user });
                    break;
            }

            _addUserViewModel.ResetPassword = true;
            _addUserViewModel.SuccessfulUserAdd?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            _addUserViewModel.FailedUserAdd?.Invoke(ex.Message);
        }


    }

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    private User CreateUserFromViewModel()
    {
        var user = _addUserViewModel.User;
        return new User()
        {
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Email = user.Email,
        };
    }
}
