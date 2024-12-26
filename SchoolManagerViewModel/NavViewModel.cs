using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagerModel.Entities.UserModel;
using System.Collections.ObjectModel;
using System.Resources;
using System.Runtime.InteropServices;
using SchoolManagerModel.Utils;
using SchoolManagerViewModel.Utils;

namespace SchoolManagerViewModel;
public partial class NavViewModel : ObservableObject
{
    public User User { get; set; }
    private ResourceManager ResourceManager { get; set; } = UIResourceFactory.GetNewResource();  

    public NavViewModel(User user) : this()
    {
        User = user;
    }
    
    public NavViewModel()
    {
        #if (DEBUG)
        User = new User
        {
            LastName = "Surname",
            FirstName = "FirstName"
        };
        #endif
        
        List<ListItemTemplate> templates = [
            new(typeof(AddUserViewModel), "PersonRegular", ResourceManager.GetStringOrDefault("AddUser")),
            new(typeof(AdminClassesViewModel), "ClassRegular", ResourceManager.GetStringOrDefault("Classes")),
            new(typeof(AddSubjectViewModel), "NotepadRegular", ResourceManager.GetStringOrDefault("AddSubject")),
        ];
        
        Items = new ObservableCollection<ListItemTemplate>(templates);
        SelectedListItem = Items.First(vm => vm.ModelType == typeof(AddUserViewModel));
        
        /*
         * On desktop, the collapsed navbar is 40 pixels width to show menu icons.
         * Icons are hidden on mobile.
         */
        if (SystemInfo.IsMobilePlatform())
        {
            CollapsedNavbarWidth = 0;
            IsPaneOpen = false;
        } 
        
    }

    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private ViewModelBase _currentPage = new AddUserViewModel();

    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    public int CollapsedNavbarWidth { get; set; } = 65;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;

        /* var vm = Design.IsDesignMode
             ? Activator.CreateInstance(value.ModelType)
             : Ioc.Default.GetService(value.ModelType);*/

        if (SystemInfo.IsMobilePlatform())
        {
            IsPaneOpen = false;
        }
        
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
