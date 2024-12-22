using Avalonia.Controls;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerAvalonia.Views;

public partial class AdminView : UserControl
{
    public AdminView(Admin admin) : this()
    {

    }

    public AdminView()
    {
        InitializeComponent();
    }
}