using SchoolManagerModel.Entities;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using SchoolManagerViewModel.Commands;
using SchoolManagerViewModel.EntityViewModels;
using System.Collections.ObjectModel;

namespace SchoolManagerViewModel;

public class AddUserViewModel : ViewModelBase
{
    #region Private Fields

    private bool _isStudent = true;
    private bool _resetPassword = false;
    private bool _allSubjectsSelected = false;
    private Role _selectedRole = Role.Student;
    private Class? _class;
    private ObservableCollection<Class> _classes = [];
    private ObservableCollection<CheckBoxListItem<Subject>> _subjects = [];
    private bool _hasSelectedClass;

    #endregion

    #region Public Properties

    public ValidatedUserViewModel User { get; }

    public Role SelectedRole
    {
        get => _selectedRole;
        set
        {
            SetField(ref _selectedRole, value, nameof(SelectedRole));
            IsStudent = _selectedRole == Role.Student;
        }
    }

    public bool IsStudent
    {
        get => _isStudent;
        set => SetField(ref _isStudent, value, nameof(IsStudent));
    }

    public bool HasSelectedClass
    {
        get => _hasSelectedClass;
        set => SetField(ref _hasSelectedClass, value, nameof(HasSelectedClass));
    }

    public ObservableCollection<Class> Classes
    {
        get => _classes;
        set => SetField(ref _classes, value, nameof(Classes));
    }

    public Class? Class
    {
        get => _class;
        set
        {
            SetField(ref _class, value, nameof(Class));
            AddUserCommand.NotifyCanExecuteChanged();
            if (_class != null)
            {
                GetClassSubjectsAsync(_class);
            }
            AllSubjectsSelected = false;
        }
    }

    public ObservableCollection<CheckBoxListItem<Subject>> Subjects
    {
        get => _subjects;
        set => SetField(ref _subjects, value, nameof(Subjects));
    }

    public bool AllSubjectsSelected
    {
        get => _allSubjectsSelected;
        set => SetField(ref _allSubjectsSelected, value, nameof(AllSubjectsSelected));
    }

    public bool ResetPassword
    {
        get => _resetPassword;
        set => SetField(ref _resetPassword, value, nameof(ResetPassword));
    }


    public Action? SuccessfulUserAdd;
    public Action<string>? FailedUserAdd;
    #endregion

    #region Commands
    public AddUserCommand AddUserCommand { get; }
    public SelectAllSubjectsCommand SelectAllSubjectsCommand { get; }
    #endregion

    #region Events
    public void SelectChange(object sender, EventArgs e)
    {
        CheckAllSelected();
    }
    #endregion

    #region Constructor
    public AddUserViewModel()
    {
        User = new ValidatedUserViewModel();
        AddUserCommand = new AddUserCommand(this);
        User.PropertyChanged += (_, _) => AddUserCommand.NotifyCanExecuteChanged();
        SelectAllSubjectsCommand = new SelectAllSubjectsCommand(this);
        GetClassesAsync();
        Class = _classes.FirstOrDefault();
    }
    #endregion

    #region Private Methods
    private async void GetClassesAsync()
    {
        using var dbContext = new SchoolDbContext();
        var db = new ClassDatabase(dbContext);
        var classManager = new ClassManager(db);
        _classes = new ObservableCollection<Class>(await classManager.GetClassesAsync());
    }

    private async void GetClassSubjectsAsync(Class cls)
    {
        using var dbContext = new SchoolDbContext();
        var db = new ClassDatabase(dbContext);
        var classManager = new ClassManager(db);
        var subjects = await classManager.GetClassSubjectsAsync(cls);

        Subjects.Clear();
        foreach (var subject in subjects)
        {
            Subjects.Add(new CheckBoxListItem<Subject> { Item = subject });
        }
    }

    private void CheckAllSelected()
    {
        AllSubjectsSelected = _subjects.All(item => item.IsChecked);
    }

    #endregion

}
