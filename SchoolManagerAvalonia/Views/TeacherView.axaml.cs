using Avalonia.Controls;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerAvalonia.Views;

public partial class TeacherView : UserControl
{
    public TeacherView(Teacher teacher) : this()
    {

    }
    public TeacherView()
    {
        InitializeComponent();
    }
}