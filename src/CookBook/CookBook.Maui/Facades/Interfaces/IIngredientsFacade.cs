using CookBook.Common.Models;

namespace CookBook.Maui.Facades
{
    public interface IIngredientsFacade
    {
        Task<ICollection<IngredientListModel>> GetIngredientsAllAsync();
        Task<IngredientDetailModel?> GetIngredientByIdAsync(Guid id);
    }
}