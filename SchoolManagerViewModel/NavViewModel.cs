using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoolManagerModel.Entities.UserModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Resources;
using System.Runtime.InteropServices;
using SchoolManagerModel.Managers;
using SchoolManagerModel.Persistence;
using SchoolManagerModel.Utils;
using SchoolManagerViewModel.Utils;

namespace SchoolManagerViewModel;
public partial class NavViewModel : ObservableObject
{
    public User User { get; set; }
    private ResourceManager ResourceManager { get; set; } = UIResourceFactory.GetNewResource();
    private List<ListItemTemplate> ListItemTemplates { get; set;  } = [];

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
        
        Items = new ObservableCollection<ListItemTemplate>(ListItemTemplates);
        
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

    async partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;

        if (SystemInfo.IsMobilePlatform())
        {
            IsPaneOpen = false;
        }
        
        var vm = Activator.CreateInstance(value.ModelType);

        if (vm is not ViewModelBase vmb)
        {
            return;
        }

        if (vm is AddShowMarksViewModel addShowMarksViewModel)
        {
            await LoadSubjects(addShowMarksViewModel);
        }
        
        CurrentPage = vmb;
    }

    public ObservableCollection<ListItemTemplate> Items { get; set; }

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

    private async Task LoadSubjects(AddShowMarksViewModel addShowMarksViewModel)
    {
        await using var dbContext = new SchoolDbContext();
        var dataHandler = new TeacherDatabase(dbContext);
        var teacherManager = new TeacherManager(dataHandler);

        var teachers = await teacherManager.GetTeacherUsersAsync();
        addShowMarksViewModel.Teacher = teachers.FirstOrDefault(x => x.User.Id == User.Id);
        
        await addShowMarksViewModel.LoadSubjectsAsync();
    }

    public required EventHandler LogoutRequested { get; set; }

    public void LoadAdminNavigationItems()
    {
        Items = new ObservableCollection<ListItemTemplate>([
            new ListItemTemplate(typeof(FilterUsersViewModel), "PersonRegular",
                ResourceManager.GetStringOrDefault("Users")),

            new ListItemTemplate(typeof(AddUserViewModel), "PersonRegular",
                ResourceManager.GetStringOrDefault("AddUser")),

            new ListItemTemplate(typeof(AdminClassesViewModel), "ClassRegular",
                ResourceManager.GetStringOrDefault("Classes")),

            new ListItemTemplate(typeof(AddSubjectViewModel), "NotepadRegular",
                ResourceManager.GetStringOrDefault("AddSubject"))
        ]);
        
        SelectedListItem = Items.First();
    }

    public void LoadTeacherNavigationItems()
    {
        Items = new ObservableCollection<ListItemTemplate>([
            new ListItemTemplate(typeof(AddShowMarksViewModel), "PersonRegular",
                ResourceManager.GetStringOrDefault("Marks"))
        ]);
        
        SelectedListItem = Items.First();
    }
    
}
