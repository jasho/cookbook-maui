using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Ingredient;

namespace CookBook.Maui.Pages;

public partial class IngredientListView
{
	public IngredientListView(
        IngredientListViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
	{
		InitializeComponent();
    }
}