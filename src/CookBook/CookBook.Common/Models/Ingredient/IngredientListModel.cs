using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

[JsonSchemaFlatten]
public class IngredientListModel(Guid id, string name, string? imageUrl = null)
    : ModelBase
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? ImageUrl { get; set; } = imageUrl;
}
