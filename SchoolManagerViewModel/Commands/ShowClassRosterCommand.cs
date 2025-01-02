using System.Windows.Input;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using SchoolManagerViewModel.EntityViewModels;

namespace SchoolManagerViewModel.Commands;

public class ShowClassRosterCommand(AdminClassesViewModel adminClassesViewModel, ClassViewModel? cls) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return cls != null;
    }

    public async void Execute(object? parameter)
    {
        var targetClass = parameter as ClassViewModel ?? cls;

        if (targetClass == null)
        {
            return;
        }

        try
        {
            await using var dbContext = new SchoolDbContext();
            var classDatabase = new ClassDatabase(dbContext);
            var classManager = new ClassManager(classDatabase);

            var currentClass = await classManager.GetClassByIdAsync(targetClass.Id);

            if (currentClass == null)
            {
                return;
            }

            var students = await classManager.GetClassStudentsAsync(currentClass);
            adminClassesViewModel.DisplayClassRoster?.Invoke(targetClass, students.Select(x => x.Name).ToList());
        }
        catch (Exception ex)
        {
            adminClassesViewModel.FailedOperation?.Invoke(ex.Message);
        }
    }

    public event EventHandler? CanExecuteChanged;
}