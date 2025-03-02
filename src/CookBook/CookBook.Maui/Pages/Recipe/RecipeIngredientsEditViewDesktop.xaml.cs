using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Recipe;

namespace CookBook.Maui.Pages;

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