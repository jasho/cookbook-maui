using CookBook.Mobile.ViewModels.Ingredient;

namespace CookBook.Mobile.Views;

public partial class IngredientEditView
{
    public IngredientEditView(IngredientEditViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
    }
}