using CookBook.Mobile.ViewModels.Recipe;

namespace CookBook.Mobile.Views;

public partial class RecipeListView
{
    public RecipeListView(RecipeListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}