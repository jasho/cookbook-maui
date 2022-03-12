using CookBook.Mobile.ViewModels.Ingredient;

namespace CookBook.Mobile.Views;

public partial class IngredientCreateView
{
    public IngredientCreateView(IngredientCreateViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
    }
}