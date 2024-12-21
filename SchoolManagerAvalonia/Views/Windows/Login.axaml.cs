using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SchoolManagerViewModel;

namespace SchoolManagerAvalonia.Views.Windows;

public partial class Login : Window
{
    public Login()
    {
        InitializeComponent();
        DataContext = new LoginViewModel();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

