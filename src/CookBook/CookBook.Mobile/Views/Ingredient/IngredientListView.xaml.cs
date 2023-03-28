using CookBook.Mobile.Services;
using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

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