using SchoolManagerViewModel.Commands;

namespace SchoolManagerViewModel;

public class LanguageSelectViewModel : ViewModelBase
{
    public ShowLoginCommand ShowLoginCommand { get; private set; }

    public Action? LoginShowAction { get; set; } = null;

    private int _languageId;

    public int LanguageId
    {
        get { return _languageId; }
        set
        {
            SetField(ref _languageId, value, "LanguageId");
        }
    }

    public LanguageSelectViewModel()
    {
        ShowLoginCommand = new ShowLoginCommand(this);
    }

}
