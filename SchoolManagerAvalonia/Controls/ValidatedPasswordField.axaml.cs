using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SchoolManagerAvalonia.Controls;

public partial class ValidatedPasswordField : UserControl
{
    public ValidatedPasswordField()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<string> LabelContentProperty =
        AvaloniaProperty.Register<ValidatedPasswordField, string>(nameof(LabelContent), string.Empty);

    public string LabelContent
    {
        get => GetValue(LabelContentProperty);
        set => SetValue(LabelContentProperty, value);
    }

    public static readonly StyledProperty<string> PasswordProperty =
        AvaloniaProperty.Register<ValidatedPasswordField, string>(nameof(Password), string.Empty);

    public string Password
    {
        get => GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    public static readonly StyledProperty<string> ValidationErrorProperty =
        AvaloniaProperty.Register<ValidatedPasswordField, string>(nameof(ValidationError), string.Empty);

    public string ValidationError
    {
        get => GetValue(ValidationErrorProperty);
        set => SetValue(ValidationErrorProperty, value);
    }

    /*public static readonly StyledProperty<bool> ResetPasswordProperty =
        AvaloniaProperty.Register<ValidatedPasswordField, bool>(nameof(ResetPassword), false, notifying: OnResetPasswordChanged);

    public bool ResetPassword
    {
        get => GetValue(ResetPasswordProperty);
        set => SetValue(ResetPasswordProperty, value);
    }

    private static void OnResetPasswordChanged(IAvaloniaObject d, bool e)
    {
        if (d is ValidatedPasswordField validatedPasswordField && e)
        {
            validatedPasswordField.ResetPasswordBox();
        }
    }

    private void ResetPasswordBox()
    {
        this.FindControl<TextBox>("PasswordField").Clear();
        Password = string.Empty;
        ResetPassword = false;
    }*/
}