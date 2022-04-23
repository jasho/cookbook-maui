using CookBook.Common.Enums;

namespace CookBook.Common.Models;

public record RecipeListModel(Guid Id, string Name, FoodType FoodType, string? ImageUrl = null)
    : ModelBase
{
}