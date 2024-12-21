using System.ComponentModel.DataAnnotations;

namespace SchoolManagerViewModel.EntityViewModels;
public class ClassViewModel : ViewModelBase
{
    private int _id;
    private string _name = string.Empty;
    private bool _isChanged = false;

    [Key]
    public int Id
    {
        get => _id;
        set
        {
            if (_id == value)
            {
                return;
            }

            _id = value;
            SetField(ref _id, value, nameof(Id));
            IsChanged = true;

        }
    }

    [Required]
    public string Name
    {
        get => _name;
        set
        {
            if (_name == value)
            {
                return;
            }

            _name = value;
            SetField(ref _name, value, nameof(Name));
            IsChanged = true;
        }
    }

    public bool IsChanged
    {
        get => _isChanged;
        set
        {
            if (_isChanged == value)
            {
                return;
            }

            _isChanged = value;
            SetField(ref _isChanged, value, nameof(IsChanged));

        }
    }

}