using System.Windows.Input;

namespace SchoolManagerViewModel.Commands;
public class ResetUserFilterCommand : ICommand
{
    private FilterUsersViewModel _filterUsersViewModel;
    public event EventHandler? CanExecuteChanged;


    public ResetUserFilterCommand(FilterUsersViewModel viewModel)
    {
        _filterUsersViewModel = viewModel;
    }


    public bool CanExecute(object? parameter)
    {
        return !(_filterUsersViewModel.UsernameFilter == string.Empty
            && _filterUsersViewModel.FirstNameFilter == string.Empty
            && _filterUsersViewModel.LastNameFilter == string.Empty
            && _filterUsersViewModel.EmailFilter == string.Empty);
    }

    public async void Execute(object? parameter)
    {
        _filterUsersViewModel.UsernameFilter = string.Empty;
        _filterUsersViewModel.FirstNameFilter = string.Empty;
        _filterUsersViewModel.LastNameFilter = string.Empty;
        _filterUsersViewModel.EmailFilter = string.Empty;

        await _filterUsersViewModel.LoadAllUser();
    }

    public void NotifyCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
