using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

public partial class IngredientListView
{
	public IngredientListView(IngredientListViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}