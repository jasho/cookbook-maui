using CookBook.Mobile.Services;
using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

public partial class RecipeIngredientsEditViewDesktop
{
	public RecipeIngredientsEditViewDesktop(
        RecipeIngredientsEditViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
	{
		InitializeComponent();
	}
}