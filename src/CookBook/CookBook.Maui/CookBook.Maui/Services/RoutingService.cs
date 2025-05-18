using CookBook.Maui.Models;
using CookBook.Maui.Pages;
using CookBook.Maui.Services.Interfaces;

namespace CookBook.Maui.Services;

public abstract class RoutingService : IRoutingService
{
    public const string RecipeListRouteAbsolute = "//recipes";
    public const string RecipeDetailRouteRelative = "/detail";
    public const string RecipeEditRouteRelative = "/edit";
    public const string RecipeIngredientsEditRouteRelative = "/ingredients";

    public const string IngredientListRouteAbsolute = "//ingredients";
    public const string IngredientDetailRouteRelative = "/detail";
    public const string IngredientEditRouteRelative = "/edit";

    public const string SettingsRouteAbsolute = "//settings";

    protected static readonly ICollection<RouteModel> RoutesCommon =
    [
        new(RecipeListRouteAbsolute, typeof(RecipeListPage)),
        new(IngredientListRouteAbsolute, typeof(IngredientListPage)),
        new(SettingsRouteAbsolute, typeof(SettingsPage))
    ];

    public abstract IEnumerable<RouteModel> Routes { get; }
}