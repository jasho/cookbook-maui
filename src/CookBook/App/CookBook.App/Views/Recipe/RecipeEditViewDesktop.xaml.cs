using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Views;

public partial class RecipeEditViewDesktop
{
	public RecipeEditViewDesktop(RecipeEditViewModel viewModel)
        : base(viewModel)
    {
		InitializeComponent();
	}
}