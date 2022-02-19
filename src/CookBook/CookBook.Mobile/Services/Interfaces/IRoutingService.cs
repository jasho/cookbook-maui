using CookBook.Mobile.Models;
using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Services
{
    public interface IRoutingService
    {
        ICollection<RouteModel> Routes { get; }

        string GetRouteByViewModel<TViewModel>()
            where TViewModel : IViewModel;
    }
}