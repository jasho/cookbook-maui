using CookBook.Maui.Models;
using CookBook.Maui.Pages;
using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels;
using CookBook.Maui.ViewModels.Ingredient;
using CookBook.Maui.ViewModels.Recipe;

namespace CookBook.Maui.Services;

public class RoutingService : IRoutingService
{
    private static ICollection<RouteModel> routesCommon = new List<RouteModel>
    {
        new("//recipes", typeof(RecipeListPage), typeof(RecipeListViewModel)),
        new("//ingredients", typeof(IngredientListPage), typeof(IngredientListViewModel)),
        new("//settings", typeof(SettingsPage), typeof(SettingsViewModel)),
    };

    private static IEnumerable<RouteModel> routesPhone = new List<RouteModel>
    {
        new("//ingredients/detail", typeof(IngredientDetailPagePhone), typeof(IngredientDetailViewModel)),
        new("//ingredients/edit", typeof(IngredientEditPagePhone), typeof(IngredientEditViewModel)),
        new("//ingredients/detail/edit", typeof(IngredientEditPagePhone), typeof(IngredientEditViewModel)),

        new("//recipes/detail", typeof(RecipeDetailPagePhone), typeof(RecipeDetailViewModel)),
    }.Concat(routesCommon);

    private static IEnumerable<RouteModel> routesDesktop = new List<RouteModel>
    {
        new("//ingredients/detail", typeof(IngredientDetailPageDesktop), typeof(IngredientDetailViewModel)),
        new("//ingredients/edit", typeof(IngredientEditPageDesktop), typeof(IngredientEditViewModel)),
        new("//ingredients/detail/edit", typeof(IngredientEditPageDesktop), typeof(IngredientEditViewModel)),

        new("//recipes/detail", typeof(RecipeDetailPageDesktop), typeof(RecipeDetailViewModel)),

        new("//recipes/detail/edit", typeof(RecipeEditPageDesktop), typeof(RecipeEditViewModel)),
        new("//recipes/edit", typeof(RecipeEditPageDesktop), typeof(RecipeEditViewModel)),

        new("//recipes/detail/edit/ingredients", typeof(RecipeIngredientsEditPageDesktop), typeof(RecipeIngredientsEditViewModel)),
        new("//recipes/edit/ingredients", typeof(RecipeIngredientsEditPageDesktop), typeof(RecipeIngredientsEditViewModel)),
    }.Concat(routesCommon);

    public IEnumerable<RouteModel> Routes
        => DeviceInfo.Idiom == DeviceIdiom.Phone
            ? routesPhone
            : routesDesktop;

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : ViewModelBase
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
