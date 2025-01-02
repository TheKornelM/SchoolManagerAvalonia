
using Avalonia.Controls;
using AvaloniaDialogs.Views;
using SchoolManagerModel.Utils;
using SchoolManagerViewModel;

namespace SchoolManagerAvalonia;

public partial class AdminClassesView : UserControl
{
    public AdminClassesView()
    {
        InitializeComponent();
        DataContext = GetNewClassesViewModel();
    }
    
    private AdminClassesViewModel GetNewClassesViewModel()
    {
        var resourceManager = UIResourceFactory.GetNewResource();
        var classesViewModel = new AdminClassesViewModel();
        
        classesViewModel.SuccessfulOperation = async (message) =>
        {
            DataContext = GetNewClassesViewModel();
            SingleActionDialog dialog = new()
            {
                Message = message,
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();
        };
        classesViewModel.FailedOperation = async (string message) =>
        {
            SingleActionDialog dialog = new()
            {
                Message = message,
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();        
        };
        classesViewModel.DisplayClassRoster = async (classViewModel, students) =>
        {
            var studentListMessage = students.Count == 0 ? resourceManager.GetStringOrDefault("NoAddedStudent") : 
                string.Join('\n', students);
            
            SingleActionDialog dialog = new()
            {
                Message = $"{classViewModel.Name} {resourceManager.GetStringOrDefault("Roster")}\n\n{studentListMessage}",
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();
        };
        
        return classesViewModel;
    }
}