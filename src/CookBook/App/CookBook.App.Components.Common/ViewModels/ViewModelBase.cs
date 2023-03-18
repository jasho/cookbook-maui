using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CookBook.App.Components.Common.ViewModels;

public partial class ViewModelBase : ObservableObject, IViewModel
{
    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private Task GoBackAsync()
    {
        if (Shell.Current.Navigation.NavigationStack.Count > 1)
        {
            Shell.Current.SendBackButtonPressed();
        }

        return Task.CompletedTask;
    }
}