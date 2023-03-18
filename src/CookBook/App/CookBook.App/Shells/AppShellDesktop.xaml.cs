using CommunityToolkit.Mvvm.Input;
using CookBook.App.Components.Common.Services;
using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Shells;

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