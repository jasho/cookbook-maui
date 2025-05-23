using Microsoft.UI.Xaml.Controls;

using Button = Microsoft.UI.Xaml.Controls.Button;
using Grid = Microsoft.UI.Xaml.Controls.Grid;
using GridLength = Microsoft.UI.Xaml.GridLength;
using GridUnitType = Microsoft.UI.Xaml.GridUnitType;
using HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment;
using ICommand = System.Windows.Input.ICommand;
using RowDefinition = Microsoft.UI.Xaml.Controls.RowDefinition;

namespace CookBook.Maui.Controls;

public partial class ButtonWithConfirmation
{
    partial void ChangedHandler(object? sender, EventArgs e)
    {
        if (sender is not ButtonWithConfirmation { Handler.PlatformView: Button platformButton } button)
        {
            return;
        }


        platformButton.Flyout = new Flyout
        {
            Content = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                },
                Children =
                {
                    GetTitleTextBlock(button.ConfirmationTitle),
                    GetMessageTextBlock(button.ConfirmationMessage),
                    GetAcceptButton(button.ConfirmationAcceptButtonText, button.Command)
                }
            }
        };

        button.Command = null;
    }

    private TextBlock GetTitleTextBlock(string text)
        => new()
        {
            Text = text
        };

    private TextBlock GetMessageTextBlock(string text)
    {
        var titleTextBlock = new TextBlock
        {
            Text = text
        };
        titleTextBlock.SetValue(Grid.RowProperty, 1);

        return titleTextBlock;
    }

    private Button GetAcceptButton(string text, ICommand command)
    {
        var acceptButton = new Button
        {
            Content = new TextBlock
            {
                Text = text
            },
            HorizontalAlignment = HorizontalAlignment.Right,
            Command = command
        };

        acceptButton.SetValue(Grid.RowProperty, 2);

        return acceptButton;
    }


    partial void ChangingHandler(object? sender, HandlerChangingEventArgs e)
    {
    }
}