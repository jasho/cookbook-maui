using CookBook.Mobile.ViewModels.Ingredient;

namespace CookBook.Mobile.Views;

public partial class IngredientListView
{
	public IngredientListView(IngredientListViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
    }
}