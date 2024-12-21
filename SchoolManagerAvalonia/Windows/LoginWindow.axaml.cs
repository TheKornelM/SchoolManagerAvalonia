using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SchoolManagerViewModel;

namespace SchoolManagerAvalonia.Views.Windows;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = new LoginViewModel();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

