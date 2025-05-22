using CookBook.Maui.Models;
using CookBook.Maui.Pages;

namespace CookBook.Maui.Services;

public class RoutingService3 : RoutingService
{
    public override IEnumerable<RouteModel> Routes
        => RoutesCommon.Concat(
        [
            new(RoutingConstants.IngredientListRouteAbsolute + RoutingConstants.IngredientDetailRouteRelative, typeof(IngredientDetailPage)),
            new(RoutingConstants.IngredientListRouteAbsolute + RoutingConstants.IngredientEditRouteRelative, typeof(IngredientEditPage)),
            new(RoutingConstants.IngredientListRouteAbsolute + RoutingConstants.IngredientDetailRouteRelative + RoutingConstants.IngredientEditRouteRelative, typeof(IngredientEditPage)),

            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeDetailRouteRelative, typeof(RecipeDetailPage)),
            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeEditRouteRelative, typeof(RecipeEditPage)),

            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeDetailRouteRelative + RoutingConstants.RecipeEditRouteRelative, typeof(RecipeIngredientsEditPage)),
            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeEditRouteRelative + RoutingConstants.RecipeIngredientsEditRouteRelative, typeof(RecipeIngredientsEditPage)),
            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeDetailRouteRelative + RoutingConstants.RecipeEditRouteRelative + RoutingConstants.RecipeIngredientsEditRouteRelative,
                typeof(RecipeIngredientsEditPage))
        ]);
}