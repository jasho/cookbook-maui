using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.BL.Mappers;
using CookBook.Api.DAL;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class IngredientAmountFacade(
    IngredientAmountMapper ingredientAmountMapper,
    IStorage storage)
    : IIngredientAmountFacade
{
    public void UpdateIngredientAmounts(Guid recipeId, IEnumerable<IngredientAmountModel> models)
    {
        var entitiesToRemove = storage.IngredientAmounts.Where(ingredientAmount => ingredientAmount.RecipeId == recipeId);
        foreach(var entityToRemove in entitiesToRemove)
        {
            storage.IngredientAmounts.Remove(storage.IngredientAmounts.First(ingredientAmount => ingredientAmount.Id == entityToRemove.Id));
        }

        var entitiesToAdd = ingredientAmountMapper.ModelsToEntities(models);
        storage.IngredientAmounts.AddRange(entitiesToAdd);
    }
}
