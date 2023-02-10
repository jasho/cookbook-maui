using CookBook.Entities.Enums;

namespace CookBook.Common.Models;

public record RecipeDetailModel : ModelBase
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public TimeSpan Duration { get; set; }
    public FoodType FoodType { get; set; }
    public IList<RecipeDetailIngredientModel>? IngredientAmounts { get; set; }
    public string? ImageUrl { get; set; }
}