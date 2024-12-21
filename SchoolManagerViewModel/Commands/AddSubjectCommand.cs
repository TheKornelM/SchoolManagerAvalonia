using SchoolManagerModel.Entities;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using System.Windows.Input;

namespace SchoolManagerViewModel.Commands;

public class AddSubjectCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private AddSubjectViewModel _addSubjectViewModel;

    public bool CanExecute(object? parameter)
    {
        return IsValidSubject();
    }

    public async void Execute(object? parameter)
    {
        using var dbContext = new SchoolDbContext();
        var subjectDatabase = new SubjectDatabase(dbContext);
        var subjectManager = new SubjectManager(subjectDatabase);

        try
        {
            var subject = new Subject()
            {
                Name = _addSubjectViewModel.SubjectName,
                // Cannot be executed with zero by checking for executable
                Class = _addSubjectViewModel.SelectedClass!,
                Teacher = _addSubjectViewModel.SelectedTeacher!,
            };
            await subjectManager.AddSubjectAsync(subject);
            _addSubjectViewModel.SubjectName = string.Empty;
            _addSubjectViewModel.SuccessfulAdd?.Invoke();
        }
        catch (Exception ex)
        {
            _addSubjectViewModel.FailedAdd?.Invoke(ex.Message);
        }
    }

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public AddSubjectCommand(AddSubjectViewModel addSubjectViewModel)
    {
        _addSubjectViewModel = addSubjectViewModel;
    }

    private bool IsValidSubject()
    {
        return _addSubjectViewModel.SelectedClass != null &&
            _addSubjectViewModel.SelectedTeacher != null &&
            !string.IsNullOrEmpty(_addSubjectViewModel.SubjectName);
    }
}
