namespace CookBook.Common.Models
{
    public record IngredientDetailModel(Guid? Id, string Name, string Description, string? ImageUrl = null) : ModelBase
    {
    }
}