using CookBook.App.Components.Common.Models;
using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Components.Common.Services;

public interface IRoutingService
{
    IEnumerable<RouteModel> Routes { get; }

    string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel;
}
