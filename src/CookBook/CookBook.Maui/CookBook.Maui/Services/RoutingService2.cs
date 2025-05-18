using CookBook.Maui.Models;
using CookBook.Maui.Pages;

namespace CookBook.Maui.Services;

public class RoutingService2 : RoutingService
{
    public override IEnumerable<RouteModel> Routes
        => RoutesCommon
#if PHONE
            .Concat(new List<RouteModel>
            {
                new(RoutingConstants.IngredientListRouteAbsolute + RoutingConstants.IngredientDetailRouteRelative, typeof(IngredientDetailPagePhone)),

                new(RoutingConstants.IngredientListRouteAbsolute + RoutingConstants.IngredientEditRouteRelative, typeof(IngredientEditPagePhone)),
                new(RoutingConstants.IngredientListRouteAbsolute + RoutingConstants.IngredientDetailRouteRelative + RoutingConstants.IngredientEditRouteRelative , typeof(IngredientEditPagePhone)),

                new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeDetailRouteRelative, typeof(RecipeDetailPagePhone)),
            });
#elif DESKTOP
        .Concat(new List<RouteModel>
        {
            new(RoutingConstants.IngredientListRouteAbsolute + RoutingConstants.IngredientDetailRouteRelative, typeof(IngredientDetailPageDesktop)),
            new(RoutingConstants.IngredientListRouteAbsolute + RoutingConstants.IngredientEditRouteRelative, typeof(IngredientEditPageDesktop)),
            new(RoutingConstants.IngredientListRouteAbsolute + RoutingConstants.IngredientDetailRouteRelative + RoutingConstants.IngredientEditRouteRelative, typeof(IngredientEditPageDesktop)),

            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeDetailRouteRelative, typeof(RecipeDetailPageDesktop)),
            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeEditRouteRelative, typeof(RecipeEditPageDesktop)),

            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeDetailRouteRelative + RoutingConstants.RecipeEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop)),
            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeEditRouteRelative + RoutingConstants.RecipeIngredientsEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop)),
            new(RoutingConstants.RecipeListRouteAbsolute + RoutingConstants.RecipeDetailRouteRelative + RoutingConstants.RecipeEditRouteRelative + RoutingConstants.RecipeIngredientsEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop)),
        });
#endif
}