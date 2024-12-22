using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using AvaloniaDialogs.Views;
using SchoolManagerAvalonia.Views;
using SchoolManagerAvalonia.Views.Windows;
using SchoolManagerModel.Persistence;
using SchoolManagerViewModel;
using System.Linq;

namespace SchoolManagerAvalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        base.Initialize();
        using var dbContext = new SchoolDbContext();
        dbContext.Database.EnsureCreated();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();

        var view = new LoginView()
        {
            DataContext = getLoginViewModel()
        };

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new LoginWindow()
            {
                Content = view
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = view;
        }
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private LoginViewModel getLoginViewModel()
    {
        var vm = new LoginViewModel();
        vm.FailedLogin += async message =>
        {
            SingleActionDialog dialog = new()
            {
                Message = message,
                ButtonText = "Ok"
            };

            await dialog.ShowAsync();
        };

        vm.ShowStudentInterface = (student) =>
        {
            ShowManagerWindow(new StudentView(student));
        };
        vm.ShowAdminInterface = (admin) =>
        {
            ShowManagerWindow(new AdminView(admin));
        };
        vm.ShowTeacherInterface = (teacher) =>
        {
            ShowManagerWindow(new TeacherView(teacher));
        };

        return vm;
    }

    private void ShowManagerWindow(UserControl userControl)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow != null)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow.Content = userControl;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = userControl;
        }
    }
}