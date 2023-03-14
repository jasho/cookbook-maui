using CookBook.App.ViewModels;

namespace CookBook.App.Views;

public class IngredientListViewOptimized : ContentPageBase
{
    public IngredientListViewOptimized(IngredientListViewModel viewModel)
        : base(viewModel)
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        TimingHelper.Log("END");
    }
}