using SchoolManagerModel.Entities;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.Validators;
using SchoolManagerViewModel.EntityViewModels;
using System.Windows.Input;

namespace SchoolManagerViewModel.Commands;

public class AddClassCommand : ICommand
{
    private AdminClassesViewModel _classesViewModel { get; set; }
    public event EventHandler? CanExecuteChanged;

    public AddClassCommand(AdminClassesViewModel viewModel)
    {
        _classesViewModel = viewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return new ClassStringValidator(_classesViewModel.ResourceManager).Validate((_classesViewModel.ClassYear, _classesViewModel.Class)).IsValid;
    }

    public async void Execute(object? parameter)
    {
        if (CanExecuteChanged == null)
        {
            return;
        }

        var nextClassId = _classesViewModel.Classes.Last().Id + 1;
        var className = $"{_classesViewModel.ClassYear}/{_classesViewModel.Class}";
        var cls = new Class() { Id = nextClassId, Name = className.ToUpper() };

        try
        {
            using var dbContext = new SchoolDbContext();
            var classDatabase = new ClassDatabase(dbContext);
            var classManager = new ClassManager(classDatabase);
            await classManager.AddClassAsync(cls);
            _classesViewModel.Classes.Add(_classesViewModel.Mapper.Map<ClassViewModel>(cls));
            _classesViewModel.SuccessfulClassAdd?.Invoke();
        }
        catch (Exception ex)
        {
            _classesViewModel.FailedOperation?.Invoke(ex.Message);
        }
    }

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

}
