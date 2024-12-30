using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using SchoolManagerViewModel.Commands;
using System.Collections.ObjectModel;
using System.Diagnostics;
using SchoolManagerModel.Utils;
using SchoolManagerModel.Validators;

namespace SchoolManagerViewModel;

public class AddSubjectViewModel : ViewModelBase
{
    #region Private fields
    private Class? _selectedClass;
    private Teacher? _selectedTeacher;
    private ObservableCollection<Class> _classes = [];
    private ObservableCollection<Teacher> _teachers = [];
    private string _subjectName = string.Empty;
    private string _subjectNameValidationErrors = string.Empty;

    #endregion

    #region Public properties
    public AddSubjectCommand AddSubjectCommand { get; private set; }

    public Class? SelectedClass
    {
        get => _selectedClass;
        set
        {
            SetField(ref _selectedClass, value, nameof(SelectedClass));
            AddSubjectCommand.NotifyCanExecuteChanged();
        }
    }

    public Teacher? SelectedTeacher
    {
        get => _selectedTeacher;
        set
        {
            SetField(ref _selectedTeacher, value, nameof(SelectedTeacher));
            AddSubjectCommand.NotifyCanExecuteChanged();
        }
    }

    public ObservableCollection<Class> Classes
    {
        get => _classes;
        private set => SetField(ref _classes, value, nameof(Classes));
    }

    public ObservableCollection<Teacher> Teachers
    {
        get => _teachers;
        private set => SetField(ref _teachers, value, nameof(Teachers));
    }

    public string SubjectName
    {
        get => _subjectName;
        set
        {
            SetField(ref _subjectName, value, nameof(SubjectName));

            var resourceManager = UIResourceFactory.GetNewResource();
            var validator = new NotEmptyValidator(resourceManager);
            
            SubjectNameValidationErrors = ValidationErrors.GetErrorsFormatted(validator.Validate(SubjectName));
            AddSubjectCommand.NotifyCanExecuteChanged();
        }
    }

    public string SubjectNameValidationErrors
    {
        get => _subjectNameValidationErrors;
        set => SetField(ref _subjectNameValidationErrors, value, nameof(SubjectNameValidationErrors));
    }


    public Action? SuccessfulAdd { get; set; }
    public Action<string>? FailedAdd { get; set; }
    #endregion

    #region Constructors
    public AddSubjectViewModel()
    {
        AddSubjectCommand = new AddSubjectCommand(this);
    }
    #endregion

    #region Public methods
    public async Task LoadClasses()
    {
        using var dbContext = new SchoolDbContext();
        var classDatabase = new ClassDatabase(dbContext);
        var classManager = new ClassManager(classDatabase);

        SetField(ref _classes, new ObservableCollection<Class>(await classManager.GetClassesAsync()), nameof(Classes));

        if (_selectedClass == null)
        {
            SelectedClass = _classes.FirstOrDefault();
        }
    }

    public async Task LoadTeachers()
    {
        using var dbContext = new SchoolDbContext();
        var teacherDatabase = new TeacherDatabase(dbContext);
        var teacherManager = new TeacherManager(teacherDatabase);

        SetField(ref _teachers, new ObservableCollection<Teacher>(await teacherManager.GetTeacherUsersAsync()), nameof(Teachers));

        if (_selectedTeacher == null)
        {
            SelectedTeacher = _teachers.FirstOrDefault();
        }
    }

    #endregion
}
