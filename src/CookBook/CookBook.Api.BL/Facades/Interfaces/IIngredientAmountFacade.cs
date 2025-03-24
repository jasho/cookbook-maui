using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades.Interfaces;

public interface IIngredientAmountFacade
{
    void UpdateIngredientAmounts(Guid recipeId, IEnumerable<IngredientAmountModel> models);
}
