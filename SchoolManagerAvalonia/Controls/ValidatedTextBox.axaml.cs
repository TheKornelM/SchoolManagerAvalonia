using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SchoolManagerAvalonia.Controls;

public partial class ValidatedTextBox : UserControl
{
    public ValidatedTextBox()
    {
        //this.DataContext = this;
        InitializeComponent();
    }

    public static readonly StyledProperty<string> LabelContentProperty =
        AvaloniaProperty.Register<ValidatedTextBox, string>(nameof(LabelContent), string.Empty);

    public string LabelContent
    {
        get => GetValue(LabelContentProperty);
        set => SetValue(LabelContentProperty, value);
    }

    public static readonly StyledProperty<string> FieldValueProperty =
        AvaloniaProperty.Register<ValidatedTextBox, string>(nameof(FieldValue), string.Empty);

    public string FieldValue
    {
        get => GetValue(FieldValueProperty);
        set => SetValue(FieldValueProperty, value);
    }

    public static readonly StyledProperty<string> ValidationErrorProperty =
        AvaloniaProperty.Register<ValidatedTextBox, string>(nameof(ValidationError), string.Empty);

    public string ValidationError
    {
        get => GetValue(ValidationErrorProperty);
        set => SetValue(ValidationErrorProperty, value);
    } 
}