using System.Resources;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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
        
        classesViewModel.SuccessfulClassAdd = async () =>
        {
            DataContext = GetNewClassesViewModel();
            SingleActionDialog dialog = new()
            {
                Message = resourceManager.GetStringOrDefault("SuccessfullyAdded"),
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();
        };
        classesViewModel.FailedClassAdd = async (string message) =>
        {
            SingleActionDialog dialog = new()
            {
                Message = message,
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();        
        };
        return classesViewModel;
    }
}