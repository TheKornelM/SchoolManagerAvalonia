using SchoolManagerModel.Entities;

namespace SchoolManagerViewModel;

public class SelectClassViewModel : ClassesViewModelBase
{
    #region Private fields
    private Class? _selectedClass;
    #endregion

    #region Public properties
    public Class? SelectedClass
    {
        get => _selectedClass;
        set => SetField(ref _selectedClass, value, nameof(SelectedClass));
    }
    #endregion
}
