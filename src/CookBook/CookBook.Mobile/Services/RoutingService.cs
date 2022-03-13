using CookBook.Mobile.Models;
using CookBook.Mobile.ViewModels;
using CookBook.Mobile.ViewModels.Ingredient;
using CookBook.Mobile.ViewModels.Recipe;
using CookBook.Mobile.Views;

namespace CookBook.Mobile.Services;

public class RoutingService : IRoutingService
{
    public ICollection<RouteModel> Routes => new List<RouteModel>
    {
        new RouteModel("//ingredients/detail", typeof(IngredientDetailView), typeof(IngredientDetailViewModel)),
        new RouteModel("//ingredients/edit", typeof(IngredientEditView), typeof(IngredientEditViewModel)),
        
        new RouteModel("//recipes/detail", typeof(RecipeDetailView), typeof(RecipeDetailViewModel)),
    };

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.Single(route => route.ViewModelType == typeof(TViewModel)).Route;
}
