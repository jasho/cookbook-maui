using CookBook.App.Components.Common.Services;

namespace CookBook.App.Components.Common.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public SettingsViewModel(INavigationService navigationService)
        : base(navigationService)
    {
    }
}