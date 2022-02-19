using CookBook.Common.Enums;

namespace CookBook.Common.Models
{
    public record RecipeDetailModel(Guid? Id, string Name, string Description, TimeSpan Duration, FoodType FoodType, IList<RecipeDetailIngredientModel> IngredientAmounts, string? ImageUrl = null)
        : ModelBase
    {
    }
}