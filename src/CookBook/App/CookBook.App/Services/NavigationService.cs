using CookBook.App.Components.Common.Services;
using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Services;

public class NavigationService : INavigationService
{
    private readonly Shell shell;
    private readonly IRoutingService routingService;

    public NavigationService(
        Shell shell,
        IRoutingService routingService)
    {
        this.shell = shell;
        this.routingService = routingService;
    }

    public async Task GoToAsync(string route)
    {
        await shell.GoToAsync(route);
    }

    public async Task GoToAsync(string route, IDictionary<string, object> parameters)
    {
        await shell.GoToAsync(route, parameters);
    }

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = routingService.GetRouteByViewModel<TViewModel>();
        await GoToAsync(route);
    }

    public async Task GoToAsync<TViewModel>(IDictionary<string, object> parameters)
        where TViewModel : IViewModel
    {
        var route = routingService.GetRouteByViewModel<TViewModel>();
        await GoToAsync(route, parameters);
    }
}