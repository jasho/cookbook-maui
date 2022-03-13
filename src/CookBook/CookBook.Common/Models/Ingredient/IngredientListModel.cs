namespace CookBook.Common.Models;

public record IngredientListModel(Guid Id, string Name, string? ImageUrl = null)
    : ModelBase
{
}
