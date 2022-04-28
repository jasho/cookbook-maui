using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

public partial class RecipeDetailViewDesktop
{
    public RecipeDetailViewDesktop(RecipeDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}