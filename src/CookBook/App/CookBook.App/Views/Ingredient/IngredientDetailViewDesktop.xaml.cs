using CookBook.App.ViewModels;

namespace CookBook.App.Views;

public partial class IngredientDetailViewDesktop
{
    public IngredientDetailViewDesktop(IngredientDetailViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
    }
}