using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Components.Views;

public partial class RecipeEditView
{
	public RecipeEditView(RecipeEditViewModel recipeEditViewModel)
		: base(recipeEditViewModel)
	{
		InitializeComponent();
	}
}