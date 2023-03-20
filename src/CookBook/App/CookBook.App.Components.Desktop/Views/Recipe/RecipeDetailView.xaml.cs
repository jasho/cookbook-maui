using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Components.Views;

public partial class RecipeDetailView
{
	public RecipeDetailView(RecipeDetailViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}
}