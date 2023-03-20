using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.App.Components.Common.Services;

namespace CookBook.App.Components.Common.ViewModels;

public partial class ViewModelBase : ObservableObject, IViewModel
{
    protected readonly INavigationService navigationService;

    public ViewModelBase(INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private Task GoBackAsync()
    {
        navigationService.GoBack();
        return Task.CompletedTask;
    }
}