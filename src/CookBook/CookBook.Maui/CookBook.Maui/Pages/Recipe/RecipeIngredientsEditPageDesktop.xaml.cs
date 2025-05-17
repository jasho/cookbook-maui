using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Recipe;

namespace CookBook.Maui.Pages;

public partial class RecipeIngredientsEditPageDesktop
{
	public RecipeIngredientsEditPageDesktop(
        RecipeIngredientsEditViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
	{
		InitializeComponent();
	}
}