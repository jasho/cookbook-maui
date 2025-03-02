using CookBook.Maui.Models;
using CookBook.Maui.ViewModels.Interfaces;

namespace CookBook.Maui.Services.Interfaces;

public interface IRoutingService
{
    IEnumerable<RouteModel> Routes { get; }

    string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel;
}
