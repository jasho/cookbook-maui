//using CookBook.Maui.Models;
//using CookBook.Maui.Pages;

//namespace CookBook.Maui.Services;

//public class RoutingService1 : RoutingService
//{
//    private static readonly IEnumerable<RouteModel> routesPhone =
//        RoutesCommon.Concat(
//            new List<RouteModel>
//        {
//            new(IngredientListRouteAbsolute + IngredientDetailRouteRelative, typeof(IngredientDetailPagePhone)),

//            new(IngredientListRouteAbsolute + IngredientEditRouteRelative, typeof(IngredientEditPagePhone)),
//            new(IngredientListRouteAbsolute + IngredientDetailRouteRelative + IngredientEditRouteRelative , typeof(IngredientEditPagePhone)),

//            new(RecipeListRouteAbsolute + RecipeDetailRouteRelative, typeof(RecipeDetailPagePhone)),

//        });

//    private static readonly IEnumerable<RouteModel> routesDesktop = 
//        RoutesCommon.Concat(
//            new List<RouteModel> 
//            {
//                new(IngredientListRouteAbsolute + IngredientDetailRouteRelative, typeof(IngredientDetailPageDesktop)),
//                new(IngredientListRouteAbsolute + IngredientEditRouteRelative, typeof(IngredientEditPageDesktop)),
//                new(IngredientListRouteAbsolute + IngredientDetailRouteRelative + IngredientEditRouteRelative, typeof(IngredientEditPageDesktop)),

//                new(RecipeListRouteAbsolute + RecipeDetailRouteRelative, typeof(RecipeDetailPageDesktop)),
//                new(RecipeListRouteAbsolute + RecipeEditRouteRelative, typeof(RecipeEditPageDesktop)),

//                new(RecipeListRouteAbsolute + RecipeDetailRouteRelative + RecipeEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop)),
//                new(RecipeListRouteAbsolute + RecipeEditRouteRelative + RecipeIngredientsEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop)),
//                new(RecipeListRouteAbsolute + RecipeDetailRouteRelative + RecipeEditRouteRelative + RecipeIngredientsEditRouteRelative, typeof(RecipeIngredientsEditPageDesktop)),
//            });

//    public override IEnumerable<RouteModel> Routes
//        => DeviceInfo.Idiom == DeviceIdiom.Phone
//            ? routesPhone
//            : routesDesktop;
//}
