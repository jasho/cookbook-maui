using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Components.Views.Ingredient;

public partial class IngredientDetailView
{
    public IngredientDetailView(IngredientDetailViewModel ingredientDetailViewModel)
        : base(ingredientDetailViewModel)
    {
        InitializeComponent();
    }
}