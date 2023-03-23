using CommunityToolkit.Mvvm.Input;
using CookBook.App.ViewModels;

namespace CookBook.App.Shells;

public partial class AppShellDesktop
{
    private readonly IRoutingService routingService;
    private readonly INavigationService navigationService;

    public AppShellDesktop(
        IRoutingService routingService,
        INavigationService navigationService)
    {
        this.routingService = routingService;
        this.navigationService = navigationService;
        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToRecipesAsync()
    {
        var route = routingService.GetRouteByViewModel<RecipeListViewModel>();
        await navigationService.GoToAsync(route);
    }

    [RelayCommand]
    private async Task GoToIngredientsAsync()
    {
        var route = routingService.GetRouteByViewModel<IngredientListViewModel>();
        await navigationService.GoToAsync(route);
    }

    [RelayCommand]
    private async Task GoToSettingsAsync()
    {
        var route = routingService.GetRouteByViewModel<SettingsViewModel>();
        await navigationService.GoToAsync(route);
    }

    [RelayCommand]
    private void Exit()
    {
        Application.Current?.Quit();
    }
}