using CookBook.App.Components.ViewModels;

namespace CookBook.App.Components.Views.Ingredient;

public partial class IngredientDetailView
{
    public IngredientDetailView(IngredientDetailViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}