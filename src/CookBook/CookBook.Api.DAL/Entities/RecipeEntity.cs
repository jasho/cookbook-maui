using CookBook.Common.Enums;

namespace CookBook.Api.DAL.Entities
{
    public record RecipeEntity : EntityBase
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<IngredientAmountEntity> IngredientAmounts { get; set; } = new List<IngredientAmountEntity>();
    }
}