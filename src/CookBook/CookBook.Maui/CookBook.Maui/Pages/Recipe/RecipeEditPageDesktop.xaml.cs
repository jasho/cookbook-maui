using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Recipe;

namespace CookBook.Maui.Pages;

public partial class RecipeEditPageDesktop
{
	public RecipeEditPageDesktop(
        RecipeEditViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
    {
		InitializeComponent();
	}
}