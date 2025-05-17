using CommunityToolkit.Mvvm.Input;
using CookBook.Maui.Services;
using CookBook.Maui.Services.Interfaces;

namespace CookBook.Maui.Shells;

public partial class AppShellDesktop
{
    private readonly IRoutingService routingService;

    public AppShellDesktop(IRoutingService routingService)
    {
        this.routingService = routingService;
        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToRecipesAsync()
    {
        await Shell.Current.GoToAsync(RoutingService.RecipeListRouteAbsolute);
    }

    [RelayCommand]
    private async Task GoToIngredientsAsync()
    {
        await Shell.Current.GoToAsync(RoutingService.IngredientListRouteAbsolute);
    }

    [RelayCommand]
    private async Task GoToSettingsAsync()
    {
        await Shell.Current.GoToAsync(RoutingService.SettingsRouteAbsolute);
    }

    [RelayCommand]
    private void Exit()
    {
        Application.Current?.Quit();
    }
}