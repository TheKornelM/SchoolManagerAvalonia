using System;
using System.Resources;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SchoolManagerModel.Utils;
using SchoolManagerViewModel;

namespace SchoolManagerAvalonia.Views;

public partial class AddUserView : UserControl
{
    public AddUserView()
    {
        InitializeComponent();
        var resourceManager = UIResourceFactory.GetNewResource();
        //Users.DataContext = getNewUserViewModel(resourceManager);
    }
    
    private AddUserViewModel getNewUserViewModel(ResourceManager resourceManager)
    {
        var vm = new AddUserViewModel();
        vm.SuccessfulUserAdd = new Action(() =>
        {
            //PasswordField.Password = string.Empty;
            //ConfirmPasswordField.Password = string.Empty;
            Users.DataContext = getNewUserViewModel(resourceManager);
            //MessageBox.Show(resourceManager.GetString("SuccessfullyRegistration"));
        });
        vm.FailedUserAdd = new Action<string>((message) =>
        {
            //MessageBox.Show(message);
        });

        return vm;
    }
}