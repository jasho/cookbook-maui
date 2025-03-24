using CookBook.Common.Enums;
using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

public class RecipeListModel : ModelBase
{
    public required string Name { get; set; }
    public FoodType FoodType { get; set; }
    public string? ImageUrl { get; set; }
}
