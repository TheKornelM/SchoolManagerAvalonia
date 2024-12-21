using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using System.Collections.ObjectModel;

namespace SchoolManagerViewModel;

public class RosterViewModel : ViewModelBase
{
    #region Public properties
    public ObservableCollection<Class> Classes { get; set; } = [];
    public ObservableCollection<User> CurrentClassRoster
    {
        get => _currentClassRoster;
        set
        {
            // _currentClassRoster = value;
            SetField(ref _currentClassRoster, value, nameof(CurrentClassRoster));
        }

    }
    public Class? SelectedClass
    {
        get => _selectedClass;
        set
        {
            if (_selectedClass == value || value == null)
            {
                return;
            }

            _selectedClass = value;
            SetField(ref _selectedClass, value, nameof(SelectedClass));
            GetClassRoster(_selectedClass);
        }
    }
    #endregion

    #region Private fields
    private Class? _selectedClass;
    private ObservableCollection<User> _currentClassRoster = [];
    #endregion

    #region Constructor
    public RosterViewModel()
    {
        GetClasses();
        SelectedClass = Classes.FirstOrDefault();
    }
    #endregion

    #region Private methods
    private async void GetClasses()
    {
        using var schoolDbContext = new SchoolDbContext();
        var classDatabase = new ClassDatabase(schoolDbContext);
        var classManager = new ClassManager(classDatabase);
        Classes = new ObservableCollection<Class>(await classManager.GetClassesAsync());
    }

    private async void GetClassRoster(Class cls)
    {
        using var schoolDbContext = new SchoolDbContext();
        var classDatabase = new ClassDatabase(schoolDbContext);
        var classManager = new ClassManager(classDatabase);
        CurrentClassRoster = new ObservableCollection<User>(await classManager.GetClassStudentsAsync(cls));
    }
    #endregion
}
