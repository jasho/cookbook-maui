using CookBook.Maui.Models;
using CookBook.Maui.Pages;
using CookBook.Maui.Services.Interfaces;

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
        new(RecipeListRouteAbsolute, typeof(RecipeListPage)),
        new(IngredientListRouteAbsolute, typeof(IngredientListPage)),
        new(SettingsRouteAbsolute, typeof(SettingsPage))
    ];

    private static readonly IEnumerable<RouteModel> routesPhone = new List<RouteModel>
    {
        new(IngredientListRouteAbsolute + IngredientDetailRouteRelative, typeof(IngredientDetailPagePhone)),

        new(IngredientListRouteAbsolute + IngredientEditRouteRelative, typeof(IngredientEditPagePhone)),
        new(IngredientListRouteAbsolute + IngredientDetailRouteRelative + IngredientEditRouteRelative , typeof(IngredientEditPagePhone)),

        new(RecipeListRouteAbsolute + RecipeDetailRouteRelative, typeof(RecipeDetailPagePhone)),

    }.Concat(routesCommon);

    private static readonly IEnumerable<RouteModel> routesDesktop = new List<RouteModel>
    {
        new(IngredientListRouteAbsolute + IngredientDetailRouteRelative, typeof(IngredientDetailPageDesktop)),
        new(IngredientListRouteAbsolute + IngredientEditRouteRelative, typeof(IngredientEditPageDesktop)),
        new(IngredientListRouteAbsolute + IngredientDetailRouteRelative + IngredientEditRouteRelative, typeof(IngredientEditPageDesktop)),

        new(RecipeListRouteAbsolute + RecipeDetailRouteRelative, typeof(RecipeDetailPageDesktop)),
        new(RecipeListRouteAbsolute + RecipeEditRouteRelative, typeof(RecipeEditPageDesktop)),

        new(RecipeListRouteAbsolute + RecipeDetailRouteRelative + RecipeEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop)),
        new(RecipeListRouteAbsolute + RecipeEditRouteRelative + RecipeIngredientsEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop)),
        new(RecipeListRouteAbsolute + RecipeDetailRouteRelative + RecipeEditRouteRelative + RecipeIngredientsEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop)),
    }.Concat(routesCommon);

    public IEnumerable<RouteModel> Routes
        => DeviceInfo.Idiom == DeviceIdiom.Phone
            ? routesPhone
            : routesDesktop;
}
