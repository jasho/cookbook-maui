namespace CookBook.Common.Models;

public record IngredientDetailModel : ModelBase
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? ImageUrl { get; set; } = null;
}
