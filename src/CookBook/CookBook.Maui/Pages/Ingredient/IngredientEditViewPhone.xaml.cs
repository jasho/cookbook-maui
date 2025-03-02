using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Ingredient;

namespace CookBook.Maui.Pages;

public partial class IngredientEditViewPhone
{
    public IngredientEditViewPhone(
        IngredientEditViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
	{
		InitializeComponent();
    }
}