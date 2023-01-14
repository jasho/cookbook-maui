using CookBook.Api.DAL.Entities;

namespace CookBook.Api.DAL;

public interface IStorage
{
    IList<IngredientEntity> Ingredients { get; }
    IList<IngredientAmountEntity> IngredientAmounts { get; }
    IList<RecipeEntity> Recipes { get; }
}