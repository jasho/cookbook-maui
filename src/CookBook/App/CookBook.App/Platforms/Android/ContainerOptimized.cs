using CookBook.App.Api;
using CookBook.App.ViewModels;
using CookBook.App.Views;
using Xamarin.Android.Net;

namespace CookBook.App
{
    public class ContainerOptimized
    {
        public Page? GetAppShell()
        {
            var httpBaseAddress = new Uri("https://app-cookbook-maui-api.azurewebsites.net/");

            var routingService = new RoutingService();

            var httpMessageHandler = new AndroidMessageHandler();
            var recipesClient = new RecipesClient(new HttpClient(httpMessageHandler) { BaseAddress = httpBaseAddress } );
            var recipeListViewModel = new RecipeListViewModel(routingService, recipesClient);

            var recipeListView = new RecipeListViewOptimized(recipeListViewModel);

            var ingredientsClient = new IngredientsClient(new HttpClient(httpMessageHandler) { BaseAddress = httpBaseAddress } );

            var ingredientListViewModel = new IngredientListViewModel(routingService, ingredientsClient);
            var ingredientListView = new IngredientListViewOptimized(ingredientListViewModel);

            return new AppShellOptimized(recipeListView, ingredientListView);
        }
    }
}