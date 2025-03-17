using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

[JsonSchemaFlatten]
public class IngredientDetailModel : ModelBase
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
}
