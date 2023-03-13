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
            bootstrapper.AddSingleton<RecipeListViewModel>();
        
            bootstrapper.AddSingleton<RecipeListView>();

            bootstrapper.AddTransient<IIngredientsClient, IngredientsClient>();
            bootstrapper.AddSingleton<IngredientListViewModel>();
            bootstrapper.AddSingleton<IngredientListView>();

            bootstrapper.AddSingleton<AppShellOptimized>();
        }
    }
}