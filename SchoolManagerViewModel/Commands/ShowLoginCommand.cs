using System.Globalization;
using System.Windows.Input;

namespace SchoolManagerViewModel.Commands;

public class ShowLoginCommand : ICommand

{
    private LanguageSelectViewModel _languageSelectViewModel;
    public event EventHandler? CanExecuteChanged;

    public ShowLoginCommand(LanguageSelectViewModel viewModel)
    {
        _languageSelectViewModel = viewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (CanExecuteChanged == null)
        {
            return;
        }

        string lang = GetLanguageCode();
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);

        if (_languageSelectViewModel.LoginShowAction != null)
        {
            _languageSelectViewModel.LoginShowAction();
        }
    }

    private string GetLanguageCode()
    {
        return _languageSelectViewModel.LanguageId switch
        {
            0 => "hu-HU",
            _ => "en-EN"
        };
    }
}
