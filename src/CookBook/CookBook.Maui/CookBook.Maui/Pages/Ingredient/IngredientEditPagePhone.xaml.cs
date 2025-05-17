using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Ingredient;

namespace CookBook.Maui.Pages;

public partial class IngredientEditPagePhone
{
    public IngredientEditPagePhone(
        IngredientEditViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
	{
		InitializeComponent();
    }
}