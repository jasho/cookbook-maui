using CookBook.App.ViewModels;

namespace CookBook.App.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IRoutingService routingService;

        public NavigationService(
            IRoutingService routingService)
        {
            this.routingService = routingService;
        }

        public async Task GoToAsync(string route)
        {
            await Shell.Current.GoToAsync(route);
        }

        public async Task GoToAsync(string route, IDictionary<string, object> parameters)
        {
            await Shell.Current.GoToAsync(route, parameters);
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

        public void GoBack()
        {
            if (Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                Shell.Current.SendBackButtonPressed();
            }
        }
    }
}