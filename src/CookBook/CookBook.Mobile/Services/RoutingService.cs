using CookBook.Mobile.Models;
using CookBook.Mobile.ViewModels;
using CookBook.Mobile.ViewModels.Ingredient;
using CookBook.Mobile.Views;

namespace CookBook.Mobile.Services;

public class RoutingService : IRoutingService
{
    public ICollection<RouteModel> Routes => new List<RouteModel>
    {
        new RouteModel("//ingredients/detail", typeof(IngredientDetailView), typeof(IngredientDetailViewModel)),
        new RouteModel("//ingredients/create", typeof(IngredientCreateView), typeof(IngredientCreateViewModel)),
    };

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.Single(route => route.ViewModelType == typeof(TViewModel)).Route;
}
