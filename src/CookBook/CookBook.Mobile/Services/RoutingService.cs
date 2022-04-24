using CookBook.Mobile.Models;
using CookBook.Mobile.ViewModels;
using CookBook.Mobile.Views;

namespace CookBook.Mobile.Services;

public class RoutingService : IRoutingService
{
    private ICollection<RouteModel> routesPhone = new List<RouteModel>
    {
        new("//ingredients/detail", typeof(IngredientDetailViewPhone), typeof(IngredientDetailViewModel)),
        new("//ingredients/edit", typeof(IngredientEditView), typeof(IngredientEditViewModel)),
        new("//ingredients/detail/edit", typeof(IngredientEditView), typeof(IngredientEditViewModel)),

        new("//recipes/detail", typeof(RecipeDetailView), typeof(RecipeDetailViewModel)),
    };

    private ICollection<RouteModel> routesDesktop = new List<RouteModel>
    {
        new("//ingredients/detail", typeof(IngredientDetailViewDesktop), typeof(IngredientDetailViewModel)),
        new("//ingredients/edit", typeof(IngredientEditView), typeof(IngredientEditViewModel)),
        new("//ingredients/detail/edit", typeof(IngredientEditView), typeof(IngredientEditViewModel)),

        new("//recipes/detail", typeof(RecipeDetailView), typeof(RecipeDetailViewModel)),
    };

    public ICollection<RouteModel> Routes
        => DeviceInfo.Idiom == DeviceIdiom.Phone
            ? routesPhone
            : routesDesktop;

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
