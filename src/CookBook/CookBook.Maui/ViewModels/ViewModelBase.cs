using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CookBook.Maui.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    protected bool ForceDataRefresh = true;

    public async Task OnAppearingAsync()
    {
        if (ForceDataRefresh)
        {
            await LoadDataAsync();

            ForceDataRefresh = false;
        }
    }

    protected virtual Task LoadDataAsync()
        => Task.CompletedTask;

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