using CookBook.App.ViewModels;

namespace CookBook.App.Views;

public partial class RecipeEditViewDesktop
{
	public RecipeEditViewDesktop(RecipeEditViewModel viewModel)
        : base(viewModel)
    {
		InitializeComponent();
	}
}