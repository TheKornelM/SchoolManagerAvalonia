using System.Windows.Input;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.Utils;
using SchoolManagerViewModel.EntityViewModels;

namespace SchoolManagerViewModel.Commands;

public class DeleteClassCommand(AdminClassesViewModel adminClassesViewModel, ClassViewModel? @class) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        if (@class == null)
        {
            return false;
        }

        try
        {
            using var dbContext = new SchoolDbContext();
            var classDatabase = new ClassDatabase(dbContext);
            var classManager = new ClassManager(classDatabase);

            var currentClass = classManager.GetClassByIdAsync(@class.Id).Result; // Blocking call

            if (currentClass == null)
            {
                return false;
            }

            var studentsTask = classManager.GetClassStudentsAsync(currentClass);
            var subjectsTask = classManager.GetClassSubjectsAsync(currentClass);

            Task.WhenAll(studentsTask, subjectsTask).Wait(); // Blocking call

            return studentsTask.Result.Count == 0 && subjectsTask.Result.Count == 0;
        }
        catch (Exception ex)
        {
            adminClassesViewModel.FailedOperation?.Invoke(ex.Message);
            return false;
        }
    }

    public async void Execute(object? parameter)
    {
        if (@class == null)
        {
            return;
        }

        try
        {
            await using var dbContext = new SchoolDbContext();
            var classDatabase = new ClassDatabase(dbContext);
            var classManager = new ClassManager(classDatabase);

            var currentClass = await classManager.GetClassByIdAsync(@class.Id);

            if (currentClass == null)
            {
                return;
            }

            await classManager.DeleteClassAsync(currentClass);
            adminClassesViewModel.Classes.Remove(@class);
                
            var resourceManager = UIResourceFactory.GetNewResource();
            adminClassesViewModel.SuccessfulOperation?.Invoke(resourceManager.GetStringOrDefault("SuccessfullyDeleted"));
        }
        catch (Exception ex)
        {
            adminClassesViewModel.FailedOperation?.Invoke(ex.Message);
        }
    }

    public event EventHandler? CanExecuteChanged;
}