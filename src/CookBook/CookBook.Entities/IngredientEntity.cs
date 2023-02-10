using SQLite;

namespace CookBook.Entities;

[Table(nameof(IngredientEntity))]
public record IngredientEntity : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}