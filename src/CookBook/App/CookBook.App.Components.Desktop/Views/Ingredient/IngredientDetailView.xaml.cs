using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Components.Views;

public partial class IngredientDetailView
{
    public IngredientDetailView(IngredientDetailViewModel ingredientDetailViewModel)
        : base(ingredientDetailViewModel)
    {
        InitializeComponent();
    }
}