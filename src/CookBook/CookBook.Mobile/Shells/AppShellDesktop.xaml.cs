using CommunityToolkit.Mvvm.Input;
using CookBook.Mobile.Services;
using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Shells;

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
        var route = routingService.GetRouteByViewModel<RecipeListViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    [RelayCommand]
    private async Task GoToIngredientsAsync()
    {
        var route = routingService.GetRouteByViewModel<IngredientListViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    [RelayCommand]
    private async Task GoToSettingsAsync()
    {
        var route = routingService.GetRouteByViewModel<SettingsViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    [RelayCommand]
    private void Exit()
    {
        Application.Current?.Quit();
    }
}