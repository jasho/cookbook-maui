using CommunityToolkit.Mvvm.Input;
using CookBook.Maui.Services;
using CookBook.Maui.Services.Interfaces;

namespace CookBook.Maui.Shells;

public partial class AppShell
{
    private readonly IRoutingService routingService;

    public AppShell(IRoutingService routingService)
    {
        this.routingService = routingService;
        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToRecipesAsync()
    {
        await Shell.Current.GoToAsync(RoutingConstants.RecipeListRouteAbsolute);
    }

    [RelayCommand]
    private async Task GoToIngredientsAsync()
    {
        await Shell.Current.GoToAsync(RoutingConstants.IngredientListRouteAbsolute);
    }

    [RelayCommand]
    private async Task GoToSettingsAsync()
    {
        await Shell.Current.GoToAsync(RoutingConstants.SettingsRouteAbsolute);
    }

    [RelayCommand]
    private void Exit()
    {
        Application.Current?.Quit();
    }
}