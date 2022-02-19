namespace CookBook.Mobile.Models;

public record IngredientDetailModel(Guid Id, string Name, string Description, string? ImageUrl = null)
{
    public IngredientDetailModel()
        : this(Guid.NewGuid(), string.Empty, string.Empty)
    {
    }
}
