using CookBook.Common.Enums;
using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

[JsonSchemaFlatten]
public record RecipeListModel : ModelBase
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public FoodType FoodType { get; set; }
    public string? ImageUrl { get; set; }
}
