using CookBook.App.ViewModels;

namespace CookBook.App.Views;

public partial class RecipeListView
{
    public RecipeListView(RecipeListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}