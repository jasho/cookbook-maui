using CommunityToolkit.Mvvm.ComponentModel;
using CookBook.Common.Enums;

namespace CookBook.Common.Models;

public partial class RecipeDetailIngredientModel : ModelBase
{
    [ObservableProperty]
    private Guid? id;

    [ObservableProperty]
    private double amount;

    [ObservableProperty]
    private Unit? unit;

    [ObservableProperty]
    private IngredientListModel? ingredient;
}