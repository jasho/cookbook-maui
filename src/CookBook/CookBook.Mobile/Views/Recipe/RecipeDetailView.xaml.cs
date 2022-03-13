using CookBook.Mobile.ViewModels.Recipe;

namespace CookBook.Mobile.Views;

public partial class RecipeDetailView
{
    public RecipeDetailView(RecipeDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}