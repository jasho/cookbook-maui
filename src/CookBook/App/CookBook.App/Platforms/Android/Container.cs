using CookBook.App.Api;
using CookBook.App.ViewModels;
using CookBook.App.Views;
using Xamarin.Android.Net;
using ZeroIoC;

namespace CookBook.App {
    public partial class Container : ZeroIoCContainer
    {
        protected override void Bootstrap(IZeroIoCContainerBootstrapper bootstrapper)
        {
            bootstrapper.AddTransient<HttpMessageHandler, AndroidMessageHandler>();

            bootstrapper.AddTransient<IRoutingService, RoutingService>();
            bootstrapper.AddSingleton<IRecipesClient, RecipesClient>();
            bootstrapper.AddTransient<RecipeListViewModel>();
            bootstrapper.AddTransient<RecipeListView>();
            bootstrapper.AddTransient<RecipeListViewOptimized>();

            bootstrapper.AddTransient<IIngredientsClient, IngredientsClient>();
            bootstrapper.AddTransient<IngredientListViewModel>();
            bootstrapper.AddTransient<IngredientListView>();
            bootstrapper.AddTransient<IngredientListViewOptimized>();

            bootstrapper.AddSingleton<AppShellOptimized>();
        }
    }
}