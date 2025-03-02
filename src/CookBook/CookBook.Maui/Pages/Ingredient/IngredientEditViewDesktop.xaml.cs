using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Ingredient;

namespace CookBook.Maui.Pages;

public partial class IngredientEditViewDesktop
{
    public IngredientEditViewDesktop(
        IngredientEditViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
	{
		InitializeComponent();
    }
}