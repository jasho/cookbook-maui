using CommunityToolkit.Mvvm.ComponentModel;
using CookBook.Common.Enums;

namespace CookBook.Common.Models;

public partial class RecipeDetailModel : ModelBase
{
    [ObservableProperty]
    private Guid? id;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private TimeSpan duration;

    [ObservableProperty]
    private FoodType foodType;

    [ObservableProperty]
    private IList<RecipeDetailIngredientModel> ingredientAmounts = new List<RecipeDetailIngredientModel>();

    [ObservableProperty]
    private string? imageUrl;
}