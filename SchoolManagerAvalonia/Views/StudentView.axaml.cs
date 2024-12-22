using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SchoolManagerAvalonia.Views;

public partial class StudentView : UserControl
{
    public StudentView()
    {
        AvaloniaXamlLoader.Load(this);
    }
}