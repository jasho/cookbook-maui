using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

[JsonSchemaFlatten]
public record IngredientListModel(Guid Id, string Name, string? ImageUrl = null)
    : ModelBase
{
}
