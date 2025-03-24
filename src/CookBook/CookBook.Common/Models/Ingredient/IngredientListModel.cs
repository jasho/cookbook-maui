using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

[JsonSchemaFlatten]
public class IngredientListModel
    : ModelBase
{
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
}
