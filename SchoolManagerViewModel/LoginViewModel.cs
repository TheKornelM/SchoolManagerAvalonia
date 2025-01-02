using SchoolManagerModel.Entities.UserModel;
using SchoolManagerViewModel.Commands;
using System.Windows.Input;

namespace SchoolManagerViewModel;

public class LoginViewModel : ViewModelBase
{
    #region Public fields
    public string Username
    {
        get { return _username; }
        set
        {
            SetField(ref _username, value, "Username");
        }
    }
    public string Password
    {
        get { return _password; }
        set
        {
            SetField(ref _password, value, "Password");
        }
    }
    public ICommand LoginUserCommand { get; set; }
    public Action<string>? FailedLogin;
    public Action<Student>? ShowStudentInterface;
    public Action<Teacher>? ShowTeacherInterface;
    public Action<Admin>? ShowAdminInterface;

    #endregion

    #region Private fields
    private string _username = String.Empty;
    private string _password = String.Empty;
    //private SecureString _password = new NetworkCredential("", String.Empty).SecurePassword;
    #endregion

    public LoginViewModel()
    {
        LoginUserCommand = new LoginUserCommand(this);
    }
}
