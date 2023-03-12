using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades.Interfaces;

public interface IIngredientFacade
{
    List<IngredientListModel> GetAll();
    IngredientDetailModel? GetById(Guid id);
    Guid Create(IngredientDetailModel ingredientModel);
    Guid? Update(IngredientDetailModel ingredientModel);
    void Delete(Guid id);
}