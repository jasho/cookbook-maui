using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Microsoft.Maui.Controls.TabbedPage;

namespace CookBook.App.Views;

public class AppShellOptimized : FlyoutPage
{
    //public AppShellOptimized(RecipeListView recipeListView, IngredientListView ingredientListView)
    public AppShellOptimized(RecipeListViewOptimized recipeListView, IngredientListViewOptimized ingredientListView)
    {
        Flyout = new AppShellFlyoutView
        {
            Title = "Test title"
        };

        var detailView = new TabbedPage
        {
            Children =
            {
                recipeListView,
                ingredientListView
            }
        };
        detailView.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

        Detail = detailView;
    }
}