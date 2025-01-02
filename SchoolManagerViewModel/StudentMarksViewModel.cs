using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using System.Collections.ObjectModel;

namespace SchoolManagerViewModel;

public class StudentMarksViewModel() : ViewModelBase
{
    #region Private fields
    private ObservableCollection<DetailedMarkViewModel> _marks = [];
    public Student Student { get; set; }

    #endregion

    #region Public properties
    public ObservableCollection<DetailedMarkViewModel> Marks
    {
        get => _marks;
        set => SetField(ref _marks, value, nameof(Marks));
    }
    #endregion

    #region Constructors
    #endregion

    #region Public methods
    public async Task LoadMarksAsync()
    {
        using var dbContext = new SchoolDbContext();
        var subjectManager = new SubjectManager(new SubjectDatabase(dbContext));

        var result = await subjectManager.GetStudentMarksAsync(Student);

        _marks.Clear();

        for (int i = result.Count - 1; i >= 0; i--)
        {
            var mark = result[i];
            _marks.Add(new DetailedMarkViewModel
            {
                Student = mark.Student,
                Subject = mark.Subject,
                SubmitDate = mark.SubmitDate,
                Grade = mark.Grade,
                Notes = mark.Notes,
            });
        }
    }
    #endregion
}
