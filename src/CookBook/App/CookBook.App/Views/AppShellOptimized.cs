namespace CookBook.App.Views;

public class AppShellOptimized : FlyoutPage
{
    public AppShellOptimized(RecipeListView recipeListView, IngredientListView ingredientListView)
    {
        Flyout = new EmptyView2
        {
            Title = "Test title"
        };

        Detail = new TabbedPage
        {
            Children =
            {
                recipeListView,
                ingredientListView
            }
        };
    }
}