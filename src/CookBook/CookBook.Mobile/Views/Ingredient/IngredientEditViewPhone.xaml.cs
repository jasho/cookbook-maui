using CookBook.Mobile.Services;
using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

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