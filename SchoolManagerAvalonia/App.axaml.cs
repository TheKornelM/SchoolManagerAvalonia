using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using SchoolManagerAvalonia.Views;
using SchoolManagerAvalonia.Views.Windows;
using SchoolManagerViewModel;
using System.Linq;

namespace SchoolManagerAvalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new LoginWindow
            {
                DataContext = getLoginViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new LoginView()
            {
                DataContext = getLoginViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
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
        //vm.FailedLogin += message => DialogHost.Show($"Failed login:\n{message}");
        /*vm.ShowStudentInterface = (student) =>
        {
            var studentUI = new Stuent(student);
            studentUI.Show();
            this.Close();
        };
        vm.ShowAdminInterface = (admin) =>
        {
            var adminUI = new AdminUI(admin);
            adminUI.Show();
            this.Close();
        };
        vm.ShowTeacherInterface = (teacher) =>
        {
            var teacherUI = new TeacherUI(teacher);
            teacherUI.Show();
            this.Close();
        };*/
        return vm;
    }
}