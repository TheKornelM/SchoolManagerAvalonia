using Avalonia.Controls;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerAvalonia.Views;

public partial class NavView : UserControl
{
    public NavView(User user) : this()
    {

    }

    public NavView()
    {
        InitializeComponent();
    }
    
}