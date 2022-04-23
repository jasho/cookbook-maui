using CookBook.Common.Enums;

namespace CookBook.Common.Models;

public record RecipeDetailIngredientModel(Guid? Id, double Amount, Unit Unit, IngredientListModel Ingredient)
    : ModelBase
{
}