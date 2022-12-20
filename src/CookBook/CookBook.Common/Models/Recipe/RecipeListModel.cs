using CookBook.Common.Enums;

namespace CookBook.Common.Models;

public record RecipeListModel : ModelBase
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public FoodType FoodType { get; set; }
    public string? ImageUrl { get; set; }
}
