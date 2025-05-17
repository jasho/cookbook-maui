using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Ingredient;

namespace CookBook.Maui.Pages;

public partial class IngredientDetailPageDesktop
{
    public IngredientDetailPageDesktop(
        IngredientDetailViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
	{
		InitializeComponent();
    }
}