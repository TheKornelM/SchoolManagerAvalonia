﻿
using SchoolManagerModel.Utils;
using SchoolManagerModel.Validators;
using SchoolManagerViewModel.Commands;
using SchoolManagerViewModel.EntityViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Resources;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;

namespace SchoolManagerViewModel;

public partial class AdminClassesViewModel : ClassesViewModelBase
{
    #region Private fields
    private string _classYear = string.Empty;
    private string _class = string.Empty;
    
    [ObservableProperty]
    private string _classValidationErrors = string.Empty;
    
    [ObservableProperty]
    private string _classYearValidationErrors = string.Empty;

    #endregion

    #region Public properties

    public string Class
    {
        get => _class;
        set
        {
            SetField(ref _class, value, nameof(Class));
            ClassValidationErrors = ValidationErrors.GetErrorsFormatted(new CharLetterValidator(ResourceManager).Validate(value));
            AddClassCommand.NotifyCanExecuteChanged();
        }
    }

    public string ClassYear
    {
        get => _classYear;
        set
        {
            SetField(ref _classYear, value, nameof(ClassYear));
            ClassYearValidationErrors = ValidationErrors.GetErrorsFormatted(new ClassYearStringValidator(ResourceManager).Validate(value));
            AddClassCommand.NotifyCanExecuteChanged();
        }
    }
    
    public ResourceManager ResourceManager { get; private set; }
    public Action<string>? SuccessfulOperation { get; set; }
    public Action<string>? FailedOperation { get; set; }
    public Action<ClassViewModel, List<string>>? DisplayClassRoster { get; set; }


    #endregion

    #region Commands
    public AddClassCommand AddClassCommand { get; set; }
    public ICommand ShowClassRosterCommand { get; set; }
    public ICommand DeleteClassCommand { get; set; }

    #endregion

    #region Constructor
    public AdminClassesViewModel()
    {
        ResourceManager = UIResourceFactory.GetNewResource();
        AddClassCommand = new AddClassCommand(this);
        ShowClassRosterCommand = new AsyncRelayCommand<ClassViewModel>(
            async @class =>
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

                    // Fetch class and students asynchronously
                    var currentClass = await classManager.GetClassByIdAsync(@class.Id);

                    if (currentClass == null)
                    {
                        return;
                    }
                    
                    var students = await classManager.GetClassStudentsAsync(currentClass);

                    // Invoke the display action with the retrieved data
                    DisplayClassRoster?.Invoke(@class, students.Select(x => x.Name).ToList());
                }
                catch (Exception ex)
                {
                    // Log or handle the exception (depending on your application's logging strategy)
                    FailedOperation?.Invoke(ex.Message);
                }
            },
            @class => @class != null
        );
        DeleteClassCommand = new AsyncRelayCommand<ClassViewModel>(async @class =>
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
                Classes.Remove(@class);
                
                var resourceManager = UIResourceFactory.GetNewResource();
                SuccessfulOperation?.Invoke(resourceManager.GetStringOrDefault("SuccessfullyDeleted"));
            }
            catch (Exception ex)
            {
                FailedOperation?.Invoke(ex.Message);
            }
        },
          CanDeleteClass);

    }

    #endregion
    
    #region Private methods
    
// Helper method for CanExecute
    private bool CanDeleteClass(ClassViewModel? @class)
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
            FailedOperation?.Invoke(ex.Message);
            return false;
        }
    }
    
    #endregion
}