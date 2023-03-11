using CookBook.App.ViewModels;

namespace CookBook.App.Views;

public partial class RecipeIngredientsEditViewDesktop
{
	public RecipeIngredientsEditViewDesktop(RecipeIngredientsEditViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}
}