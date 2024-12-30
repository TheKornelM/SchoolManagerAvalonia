using System;
using System.Resources;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaDialogs.Views;
using SchoolManagerModel.Utils;
using SchoolManagerViewModel;

namespace SchoolManagerAvalonia.Views;

public partial class AddUserView : UserControl
{
    public AddUserView()
    {
        InitializeComponent();
        var resourceManager = UIResourceFactory.GetNewResource();
        DataContext = getNewUserViewModel(resourceManager);
    }
    
    private AddUserViewModel getNewUserViewModel(ResourceManager resourceManager)
    {

        var vm = DataContext as AddUserViewModel;
        vm = new AddUserViewModel();
        
        vm.SuccessfulUserAdd = async () =>
        {
            DataContext = getNewUserViewModel(resourceManager);
            SingleActionDialog dialog = new()
            {
                Message = resourceManager.GetStringOrDefault("SuccessfullyRegistration"),
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();
        };
        vm.FailedUserAdd = async (message) =>
        {
            SingleActionDialog dialog = new()
            {
                Message = message,
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();
        };

        return vm;
    }
}