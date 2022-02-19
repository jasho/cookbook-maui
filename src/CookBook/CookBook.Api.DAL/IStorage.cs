using CookBook.Api.Entities;

namespace CookBook.Api
{
    public interface IStorage
    {
        IList<IngredientEntity> Ingredients { get; }
        IList<IngredientAmountEntity> IngredientAmounts { get; }
        IList<RecipeEntity> Recipes { get; }
    }
}