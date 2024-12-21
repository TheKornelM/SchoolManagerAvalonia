using SchoolManagerModel.Entities;
using SchoolManagerModel.Entities.UserModel;
using SchoolManagerModel.Utils;

namespace SchoolManagerViewModel;

public class MarkViewModel : ViewModelBase
{
    private int _grade;
    private Student? _student;
    private Subject? _subject;
    private DateTime _submitDate;
    private string _notes = string.Empty;

    public int Grade
    {
        get => _grade;
        set => SetField(ref _grade, value, nameof(Grade));
    }

    public Student? Student
    {
        get => _student;
        set => SetField(ref _student, value, nameof(Student));
    }

    public Subject? Subject
    {
        get => _subject;
        set => SetField(ref _subject, value, nameof(Subject));
    }

    public string Notes
    {
        get => _notes;
        set => SetField(ref _notes, value, nameof(Notes));
    }

    public DateTime SubmitDate
    {
        get => _submitDate;
        set => SetField(ref _submitDate, value, nameof(SubmitDate));
    }

    public string Date => CultureUtils.GetTimeString(_submitDate);
}