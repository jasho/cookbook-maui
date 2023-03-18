using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public override Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }
}