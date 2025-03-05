using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

[JsonSchemaFlatten]
public class IngredientDetailModel(Guid? id, string name, string description, string? imageUrl = null) : ModelBase
{
    public Guid? Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string? ImageUrl { get; set; } = imageUrl;
}
