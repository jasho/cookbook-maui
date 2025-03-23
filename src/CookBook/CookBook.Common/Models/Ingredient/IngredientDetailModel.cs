using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

public class IngredientDetailModel : ModelBase
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
