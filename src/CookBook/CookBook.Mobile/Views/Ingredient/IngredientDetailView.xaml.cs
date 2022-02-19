using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

public partial class IngredientDetailView
{
    public IngredientDetailView(IngredientDetailViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}