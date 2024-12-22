using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using System.Diagnostics;
using System.Windows.Input;

namespace SchoolManagerViewModel.Commands;

public class LoginUserCommand : ICommand
{
    private LoginViewModel _viewModel;
    public event EventHandler? CanExecuteChanged;

    internal LoginUserCommand(LoginViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        if (CanExecuteChanged == null)
        {
            return;
        }


        using var schoolDbContext = new SchoolDbContext();
        var dataHandler = new UserDatabase(schoolDbContext);
        var loginManager = new LoginManager(dataHandler);
        var userManager = new UserManager(dataHandler);
        try
        {
            await loginManager.LoginAsync(_viewModel.Username, _viewModel.Password);
            User? user = await userManager.GetUserByUsernameAsync(_viewModel.Username);

            if (user == null)
            {
                return;
            }

            var role = await userManager.GetRoleAsync(user);

            // Move to function
            switch (role)
            {
                case Role.Student:
                    _viewModel.ShowStudentInterface?.Invoke(await userManager.GetStudentByUserAsync(user));
                    break;
                case Role.Teacher:
                    _viewModel.ShowTeacherInterface?.Invoke(new Teacher() { User = user });
                    break;
                case Role.Administrator:
                    _viewModel.ShowAdminInterface?.Invoke(new Admin() { User = user });
                    break;
                default:
                    _viewModel.FailedLogin?.Invoke("Unknown role.");
                    break;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            _viewModel.FailedLogin?.Invoke(ex.Message);
        }
    }
}