using CookBook.Common.Models;
using CookBook.Maui.Facades.Interfaces;

namespace CookBook.Maui.Facades;

public interface IIngredientsFacade : IFacade<IngredientListModel, IngredientDetailModel>
{

    Task<IngredientDetailModel?> GetIngredientByIdAsync(Guid id);
}