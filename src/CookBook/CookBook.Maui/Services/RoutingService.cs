using CookBook.Maui.Models;
using CookBook.Maui.Pages;
using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels;
using CookBook.Maui.ViewModels.Ingredient;
using CookBook.Maui.ViewModels.Recipe;

namespace CookBook.Maui.Services;

public class RoutingService : IRoutingService
{
    public const string RecipeListRouteAbsolute = "//recipes";
    public const string RecipeDetailRouteRelative = "/detail";
    public const string RecipeEditRouteRelative = "/edit";
    public const string RecipeIngredientsEditRouteRelative = "/ingredients";

    public const string IngredientListRouteAbsolute = "//ingredients";
    public const string IngredientDetailRouteRelative = "/detail";
    public const string IngredientEditRouteRelative = "/edit";

    public const string SettingsRouteAbsolute = "//settings";


    private static readonly ICollection<RouteModel> routesCommon =
    [
        new(RecipeListRouteAbsolute, typeof(RecipeListPage), typeof(RecipeListViewModel)),
        new(IngredientListRouteAbsolute, typeof(IngredientListPage), typeof(IngredientListViewModel)),
        new(SettingsRouteAbsolute, typeof(SettingsPage), typeof(SettingsViewModel))
    ];

    private static readonly IEnumerable<RouteModel> routesPhone = new List<RouteModel>
    {
        new(IngredientListRouteAbsolute + IngredientDetailRouteRelative, typeof(IngredientDetailPagePhone), typeof(IngredientDetailViewModel)),

        new(IngredientListRouteAbsolute + IngredientEditRouteRelative, typeof(IngredientEditPagePhone), typeof(IngredientEditViewModel)),
        new(IngredientListRouteAbsolute + IngredientDetailRouteRelative + IngredientEditRouteRelative , typeof(IngredientEditPagePhone), typeof(IngredientEditViewModel)),

        new(RecipeListRouteAbsolute + RecipeDetailRouteRelative, typeof(RecipeDetailPagePhone), typeof(RecipeDetailViewModel)),

    }.Concat(routesCommon);

    private static readonly IEnumerable<RouteModel> routesDesktop = new List<RouteModel>
    {
        new(IngredientListRouteAbsolute + IngredientDetailRouteRelative, typeof(IngredientDetailPageDesktop), typeof(IngredientDetailViewModel)),
        new(IngredientListRouteAbsolute + IngredientEditRouteRelative, typeof(IngredientEditPageDesktop), typeof(IngredientEditViewModel)),
        new(IngredientListRouteAbsolute + IngredientDetailRouteRelative + IngredientEditRouteRelative, typeof(IngredientEditPageDesktop), typeof(IngredientEditViewModel)),

        new(RecipeListRouteAbsolute + RecipeDetailRouteRelative, typeof(RecipeDetailPageDesktop), typeof(RecipeDetailViewModel)),
        new(RecipeListRouteAbsolute + RecipeEditRouteRelative, typeof(RecipeEditPageDesktop), typeof(RecipeEditViewModel)),

        new(RecipeListRouteAbsolute + RecipeDetailRouteRelative + RecipeEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop), typeof(RecipeIngredientsEditViewModel)),
        new(RecipeListRouteAbsolute + RecipeEditRouteRelative + RecipeIngredientsEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop), typeof(RecipeIngredientsEditViewModel)),
        new(RecipeListRouteAbsolute + RecipeDetailRouteRelative + RecipeEditRouteRelative + RecipeIngredientsEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop), typeof(RecipeIngredientsEditViewModel)),
    }.Concat(routesCommon);

    public IEnumerable<RouteModel> Routes
        => DeviceInfo.Idiom == DeviceIdiom.Phone
            ? routesPhone
            : routesDesktop;
}
