using CookBook.Mobile.ViewModels.Ingredient;

namespace CookBook.Mobile.Views;

public partial class IngredientDetailView
{
    public IngredientDetailView(IngredientDetailViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
    }
}