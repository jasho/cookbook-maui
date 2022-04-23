using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

public partial class RecipeDetailView
{
    public RecipeDetailView(RecipeDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}