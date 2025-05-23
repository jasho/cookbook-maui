using System.Windows.Input;

namespace CookBook.Maui.Controls;

public partial class ButtonWithConfirmation
{
    private ICommand popupCommand;

    partial void ChangedHandler(object? sender, EventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }

        popupCommand = button.Command;
        button.Command = null;

        button.Clicked += Button_Clicked;
    }

    partial void ChangingHandler(object? sender, HandlerChangingEventArgs e)
    {
        if (sender is not Button button || e.OldHandler is null)
        {
            return;
        }

        button.Clicked -= Button_Clicked;
    }

    private async void Button_Clicked(object? sender, EventArgs e)
    {
        if (sender is not ButtonWithConfirmation button)
        {
            return;
        }

        if (await Application.Current.MainPage.DisplayAlert(
                button.ConfirmationTitle,
                button.ConfirmationMessage,
                button.ConfirmationAcceptButtonText,
                button.ConfirmationCancelButtonText)
            && popupCommand.CanExecute(button.CommandParameter))
        {
            popupCommand.Execute(button.CommandParameter);
        }
    }
}