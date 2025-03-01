using CookBook.Mobile.Services;
using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

public partial class RecipeEditViewDesktop
{
	public RecipeEditViewDesktop(
        RecipeEditViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
    {
		InitializeComponent();
	}
}