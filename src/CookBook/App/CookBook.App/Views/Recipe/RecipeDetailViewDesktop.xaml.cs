using CookBook.App.ViewModels;

namespace CookBook.App.Views;

public partial class RecipeDetailViewDesktop
{
    public RecipeDetailViewDesktop(RecipeDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}