using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagerModel.Entities.UserModel;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using SchoolManagerViewModel.Utils;

namespace SchoolManagerViewModel;
public partial class NavViewModel : ObservableObject
{
    private User? _user;

    public NavViewModel(User user)
    {
        _user = user;
        /*messenger.Register<NavViewModel, LoginSuccessMessage>(this, (_, message) =>
        {
            CurrentPage = new SecretViewModel(message.Value);
        });*/

        Items = new ObservableCollection<ListItemTemplate>(_templates);

        SelectedListItem = Items.First(vm => vm.ModelType == typeof(AddUserViewModel));

        /*
         * On desktop, the collapsed navbar is 40 pixels width to show menu icons.
         * Icons are hidden on mobile.
         */
        if (SystemInfo.IsMobilePlatform())
        {
            CollapsedNavbarWidth = 0;
        }
    }

    private readonly List<ListItemTemplate> _templates =
    [
        new ListItemTemplate(typeof(AddUserViewModel), "PersonRegular", "Home"),
        new ListItemTemplate(typeof(AdminClassesViewModel), "ClassRegular", "Classes"),
        new ListItemTemplate(typeof(AddSubjectViewModel), "NotepadRegular", "Add subject")
        /*new ListItemTemplate(typeof(ButtonPageViewModel), "CursorHoverRegular", "Buttons"),
        new ListItemTemplate(typeof(TextPageViewModel), "TextNumberFormatRegular", "Text"),
        new ListItemTemplate(typeof(ValueSelectionPageViewModel), "CalendarCheckmarkRegular", "Value Selection"),*/
    ];

    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private ViewModelBase _currentPage = new AddUserViewModel();

    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    public int CollapsedNavbarWidth { get; set; } = 40;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;

        /* var vm = Design.IsDesignMode
             ? Activator.CreateInstance(value.ModelType)
             : Ioc.Default.GetService(value.ModelType);*/

        var vm = Activator.CreateInstance(value.ModelType);

        if (vm is not ViewModelBase vmb) return;

        CurrentPage = vmb;
    }

    public ObservableCollection<ListItemTemplate> Items { get; }

    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    [RelayCommand]
    private void TriggerLogout()
    {
        LogoutRequested?.Invoke(this, EventArgs.Empty);
    }

    public required EventHandler LogoutRequested { get; set; }
}
