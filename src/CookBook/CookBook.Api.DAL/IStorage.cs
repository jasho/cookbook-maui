using CookBook.Api.DAL.Entities;

namespace CookBook.Api.DAL;

public interface IStorage
{
    List<IngredientEntity> Ingredients { get; }
    List<IngredientAmountEntity> IngredientAmounts { get; }
    List<RecipeEntity> Recipes { get; }
    List<ImageEntity> Images { get; }
}