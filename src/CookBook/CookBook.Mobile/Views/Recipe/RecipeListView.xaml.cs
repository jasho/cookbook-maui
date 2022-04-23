using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

public partial class RecipeListView
{
    public RecipeListView(RecipeListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}