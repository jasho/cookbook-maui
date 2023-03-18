using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Views;

public partial class IngredientListView
{
    public IngredientListView(IngredientListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}