using AutoMapper;
using SchoolManagerModel.Entities;
using SchoolManagerModel.Utils;
using SchoolManagerModel.Validators;
using SchoolManagerViewModel.Commands;
using SchoolManagerViewModel.EntityViewModels;
using System.Collections.ObjectModel;
using System.Resources;

namespace SchoolManagerViewModel;

public class AdminClassesViewModel : ClassesViewModelBase
{
    #region Private fields
    private ObservableCollection<ClassViewModel> _classes = [];
    private string _classYear = string.Empty;
    private string _class = string.Empty;
    private string _classValidationErrors = string.Empty;
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

    public string ClassValidationErrors
    {
        get => _classValidationErrors;
        set
        {
            SetField(ref _classValidationErrors, value, nameof(ClassValidationErrors));
        }
    }

    public string ClassYearValidationErrors
    {
        get => _classYearValidationErrors;
        set
        {
            SetField(ref _classYearValidationErrors, value, nameof(ClassYearValidationErrors));
        }
    }

    public ResourceManager ResourceManager { get; private set; }
    public Action? SuccessfulClassAdd { get; set; }
    public Action<string>? FailedClassAdd { get; set; }


    #endregion

    #region Commands
    public AddClassCommand AddClassCommand { get; set; }

    #endregion

    #region Constructor
    public AdminClassesViewModel(ResourceManager resourceManager)
    {
        ResourceManager = resourceManager;
        AddClassCommand = new AddClassCommand(this);
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Class, ClassViewModel>();
            cfg.CreateMap<ClassViewModel, Class>();
        });

    }

    #endregion

    #region Private methods

    #endregion
}