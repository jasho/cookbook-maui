using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CookBook.App.ViewModels;

public abstract partial class ViewModelBase : ObservableObject, IViewModel
{
    protected readonly INavigationService navigationService;

    protected ViewModelBase(INavigationService navigationService)
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