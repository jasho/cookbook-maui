using CookBook.Maui.Models;
using CookBook.Maui.Pages;
using CookBook.Maui.Services.Interfaces;

namespace CookBook.Maui.Services;

public abstract class RoutingService : IRoutingService
{
    protected static readonly ICollection<RouteModel> RoutesCommon =
    [
        new(RoutingConstants.RecipeListRouteAbsolute, typeof(RecipeListPage)),
        new(RoutingConstants.IngredientListRouteAbsolute, typeof(IngredientListPage)),
        new(RoutingConstants.SettingsRouteAbsolute, typeof(SettingsPage))
    ];

    public abstract IEnumerable<RouteModel> Routes { get; }
}