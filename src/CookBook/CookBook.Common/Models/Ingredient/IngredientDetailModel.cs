using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

[JsonSchemaFlatten]
public record IngredientDetailModel(Guid? Id, string Name, string Description, string? ImageUrl = null) : ModelBase
{
}
