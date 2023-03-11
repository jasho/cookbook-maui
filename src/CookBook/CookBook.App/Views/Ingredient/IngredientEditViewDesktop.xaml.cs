using CookBook.App.ViewModels;

namespace CookBook.App.Views;

public partial class IngredientEditViewDesktop
{
    public IngredientEditViewDesktop(IngredientEditViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
    }
}