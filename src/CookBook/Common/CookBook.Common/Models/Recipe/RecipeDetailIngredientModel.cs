using CookBook.Common.Enums;

namespace CookBook.Common.Models;

public record RecipeDetailIngredientModel : ModelBase
{
    public Guid? Id { get; set; }
    public double Amount { get; set; }
    public Unit Unit { get; set; }
    public IngredientListModel? Ingredient { get; set; }
}