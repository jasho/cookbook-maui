using CommunityToolkit.Mvvm.Input;
using CookBook.App.Components.Common.Services;
using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Shells;

public partial class AppShellDesktop
{
    private readonly INavigationService navigationService;

    public AppShellDesktop(INavigationService navigationService)
    {
        this.navigationService = navigationService;
        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToRecipesAsync()
    {
        await navigationService.GoToAsync<RecipeListViewModel>();
    }

    [RelayCommand]
    private async Task GoToIngredientsAsync()
    {
        await navigationService.GoToAsync<IngredientListViewModel>();
    }

    [RelayCommand]
    private async Task GoToSettingsAsync()
    {
        await navigationService.GoToAsync<SettingsViewModel>();
    }

    [RelayCommand]
    private void Exit()
    {
        Application.Current?.Quit();
    }
}