
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
using RelayCommand = SchoolManagerWPF.Commands.RelayCommand;

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
        ShowClassRosterCommand = GetShowClassRosterCommand();
        DeleteClassCommand = GetDeleteClassCommand();
    }
    
    #endregion
    
    #region Private methods
    private RelayCommand<ClassViewModel> GetShowClassRosterCommand()
    {
        return new RelayCommand<ClassViewModel>(
            cls =>
            {
                var command = new ShowClassRosterCommand(this, cls);
                command.Execute(null);
            },
            cls =>
            {
                var command = new ShowClassRosterCommand(this, cls);
                return command.CanExecute(null);
            });
    }

    private RelayCommand<ClassViewModel> GetDeleteClassCommand()
    {
        return new RelayCommand<ClassViewModel>(
            @class =>
            {
                var command = new DeleteClassCommand(this, @class);
                command.Execute(null);
            },
            @class =>
            {
                var command = new DeleteClassCommand(this, @class);
                return command.CanExecute(null);
            });
    }

    #endregion
}