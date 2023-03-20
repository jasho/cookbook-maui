using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Components.Common.Services;

public interface INavigationService
{
    Task GoToAsync(string route);
    Task GoToAsync(string route, IDictionary<string, object> parameters);

    Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel;

    Task GoToAsync<TViewModel>(IDictionary<string, object> parameters)
        where TViewModel : IViewModel;

    void GoBack();
}