using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Components.Views;

public partial class IngredientEditView
{
	public IngredientEditView(IngredientEditViewModel ingredientEditViewModel)
		: base(ingredientEditViewModel)
	{
		InitializeComponent();
	}
}