using System.ComponentModel;
using SchoolManagerModel.Entities;

namespace SchoolManagerViewModel;

public class CheckBoxListItem : INotifyPropertyChanged
{
    private bool _isChecked;

    public required Subject Item { get; set; }

    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            if (_isChecked != value)
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
