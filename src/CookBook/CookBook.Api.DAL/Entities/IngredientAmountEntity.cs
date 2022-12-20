using CookBook.Common.Enums;

namespace CookBook.Api.DAL.Entities
{
    public record IngredientAmountEntity : EntityBase
    {
        public double Amount { get; set; }
        public Unit Unit { get; set; }

        public Guid? RecipeId { get; set; }
        public RecipeEntity? Recipe { get; set; }

        public Guid? IngredientId { get; set; }
        public IngredientEntity? Ingredient { get; set; }
    }
}