using CookBook.Maui.ViewModels.Interfaces;

namespace CookBook.Maui.ViewModels;

public class SettingsViewModel : IViewModel
{
    public Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }
}