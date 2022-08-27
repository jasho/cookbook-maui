using CookBook.Mobile.ViewModels;

namespace CookBook.Mobile.Views;

public partial class RecipeEditViewDesktop
{
	public RecipeEditViewDesktop(RecipeEditViewModel viewModel)
        : base(viewModel)
    {
		InitializeComponent();
	}
}