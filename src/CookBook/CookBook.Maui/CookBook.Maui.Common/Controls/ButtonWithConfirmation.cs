namespace CookBook.Maui.Controls;

public partial class ButtonWithConfirmation : Button
{
    partial void ChangedHandler(object? sender, EventArgs e);
    partial void ChangingHandler(object? sender, HandlerChangingEventArgs e);

    public string ConfirmationTitle
    {
        get => (string)GetValue(ConfirmationTitleProperty);
        set => SetValue(ConfirmationTitleProperty, value);
    }

    public string ConfirmationMessage
    {
        get => (string)GetValue(ConfirmationMessageProperty);
        set => SetValue(ConfirmationMessageProperty, value);
    }

    public string ConfirmationAcceptButtonText
    {
        get => (string)GetValue(ConfirmationAcceptButtonTextProperty);
        set => SetValue(ConfirmationAcceptButtonTextProperty, value);
    }

    public string ConfirmationCancelButtonText
    {
        get => (string)GetValue(ConfirmationCancelButtonTextProperty);
        set => SetValue(ConfirmationCancelButtonTextProperty, value);
    }

    public static readonly BindableProperty ConfirmationTitleProperty = BindableProperty.Create(nameof(ConfirmationTitle), typeof(string), typeof(ButtonWithConfirmation));
    public static readonly BindableProperty ConfirmationMessageProperty = BindableProperty.Create(nameof(ConfirmationMessage), typeof(string), typeof(ButtonWithConfirmation));
    public static readonly BindableProperty ConfirmationAcceptButtonTextProperty = BindableProperty.Create(nameof(ConfirmationAcceptButtonText), typeof(string), typeof(ButtonWithConfirmation));
    public static readonly BindableProperty ConfirmationCancelButtonTextProperty = BindableProperty.Create(nameof(ConfirmationCancelButtonText), typeof(string), typeof(ButtonWithConfirmation));

    public ButtonWithConfirmation()
    {
        HandlerChanged += ChangedHandler;
        HandlerChanging += ChangingHandler;
    }
}