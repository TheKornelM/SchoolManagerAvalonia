using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace SchoolManagerViewModel;

public class AddShowMarksViewModel : ViewModelBase
{
    #region Private Fields
    private bool _isSubjectSelected;
    private bool _isStudentSelected;
    private ObservableCollection<int> _grades = new() { 1, 2, 3, 4, 5 };
    public Teacher? Teacher { get; set; }
    private ObservableCollection<Subject> _subjects = [];
    private ObservableCollection<Student> _students = [];
    private ObservableCollection<Mark> _studentSubjectMarks = [];

    #endregion

    #region Public Properties
    public ObservableCollection<Subject> Subjects
    {
        get => _subjects;
        set => SetField(ref _subjects, value, nameof(Subjects));
    }
    public ObservableCollection<Student> Students
    {
        get => _students;
        set => SetField(ref _students, value, nameof(Students));
    }
    public MarkViewModel Mark { get; set; } = new();
    public Action<string>? SuccessfulAdd { get; set; }
    public Action<string>? FailedAdd { get; set; }
    public ObservableCollection<Mark> StudentSubjectMarks
    {
        get => _studentSubjectMarks;
        set => SetField(ref _studentSubjectMarks, value, nameof(StudentSubjectMarks));
    }

    public bool IsSubjectSelected
    {
        get => Mark.Subject != null;
        set
        {
            _isSubjectSelected = value;
            OnPropertyChanged(nameof(IsSubjectSelected));
        }
    }

    public bool IsStudentSelected
    {
        get => Mark.Student != null;
        set
        {
            _isStudentSelected = value;
            OnPropertyChanged(nameof(IsStudentSelected));
        }
    }

    public ObservableCollection<int> Grades
    {
        get => _grades;
        set => SetField(ref _grades, value, nameof(Grades));
    }

    public ICommand AddMarkCommand { get; }
    #endregion

    #region Constructor
    public AddShowMarksViewModel()
    {
        //_teacher = teacher;
        AddMarkCommand = new RelayCommand(AddMark, CanAddMark);
        Mark.Grade = 1;
        Mark.PropertyChanged += async (_, e) =>
        {
            Debug.WriteLine("Changed property: " + e.PropertyName);
            if (e.PropertyName == nameof(Mark.Subject))
            {
                IsSubjectSelected = Mark.Subject != null;
                await LoadStudentsAsync();
            }

            if (e.PropertyName == nameof(Mark.Student))
            {
                IsStudentSelected = Mark.Student != null;

                if (Mark.Student == null || Mark.Subject == null)
                {
                    StudentSubjectMarks = [];
                    return;
                }

                await LoadMarksAsync(Mark.Student, Mark.Subject);
            }
        };
    }
    #endregion

    #region Public methods
    public async Task LoadSubjectsAsync()
    {
        if (Teacher == null)
        {
            return;
        }
        
        await using var dbContext = new SchoolDbContext();
        var teacherDatabase = new TeacherDatabase(dbContext);
        var teacherManager = new TeacherManager(teacherDatabase);

        var result = await teacherManager.GetCurrentTaughtSubjectsAsync(Teacher);
        Subjects = new ObservableCollection<Subject>(result);
        Mark.Subject = Subjects.FirstOrDefault();
    }

    #endregion

    #region Private methods

    private async Task LoadStudentsAsync()
    {
        using var dbContext = new SchoolDbContext();
        var subjectsDatabase = new SubjectDatabase(dbContext);
        var subjectsManager = new SubjectManager(subjectsDatabase);

        var result = await subjectsManager.GetSubjectStudentsAsync(Mark.Subject
            ?? throw new NullReferenceException());

        SetField(ref _students, new ObservableCollection<Student>(result), nameof(Students));
        Mark.Student = Students.FirstOrDefault();
    }

    private async Task LoadMarksAsync(Student student, Subject subject)
    {
        using var dbContext = new SchoolDbContext();
        var subjectDatabase = new SubjectDatabase(dbContext);
        var subjectsManager = new SubjectManager(subjectDatabase);

        var result = await subjectsManager.GetStudentSubjectMarksAsync(student, subject);
        SetField(ref _studentSubjectMarks, new ObservableCollection<Mark>(result), nameof(StudentSubjectMarks));
    }

    private async void AddMark()
    {
        using var dbContext = new SchoolDbContext();
        var subjectsDatabase = new SubjectDatabase(dbContext);
        var subjectsManager = new SubjectManager(subjectsDatabase);

        var resourceManager = UIResourceFactory.GetNewResource();
        try
        {
            if (Mark.Student == null || Mark.Subject == null)
            {
                var errorMessage = UIResourceFactory
                    .GetNewResource().GetString("UnexpectedError")
                    ?? "Unexpected error happened";
                FailedAdd?.Invoke(errorMessage);
                return;
            }

            await subjectsManager.AddSubjectMarkAsync(new Mark()
            {
                Student = Mark.Student,
                Subject = Mark.Subject,
                Grade = Mark.Grade,
                Notes = Mark.Notes,
                SubmitDate = DateTime.Now
            });

            await LoadMarksAsync(Mark.Student, Mark.Subject);
            SuccessfulAdd?.Invoke(resourceManager.GetString("SuccessfullyAdded")
                ?? "Added successfully");
        }
        catch (Exception ex)
        {
            FailedAdd?.Invoke(ex.Message);
        }

    }

    private bool CanAddMark()
    {
        return Mark.Subject != null && Mark.Student != null;
    }

    #endregion
}
