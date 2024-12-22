using Avalonia.Controls;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerAvalonia.Views;

public partial class StudentView : UserControl
{
    public StudentView(Student student) : this()
    {
    }

    public StudentView()
    {
        InitializeComponent();
    }
}