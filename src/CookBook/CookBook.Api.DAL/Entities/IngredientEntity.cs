namespace CookBook.Api.DAL.Entities
{
    public record IngredientEntity : EntityBase
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}