using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace SchoolManagerViewModel;

public class DetailedMarkViewModel : MarkViewModel
{
    private bool _isDetailsVisible;
    public bool IsDetailsVisible
    {
        get => _isDetailsVisible;
        set => SetField(ref _isDetailsVisible, value, nameof(IsDetailsVisible));
    }

    public ICommand ToggleDetailsCommand { get; }

    public DetailedMarkViewModel()
    {
        ToggleDetailsCommand = new RelayCommand(ToggleDetails);
    }

    private void ToggleDetails()
    {
        IsDetailsVisible = !IsDetailsVisible;
    }
}
