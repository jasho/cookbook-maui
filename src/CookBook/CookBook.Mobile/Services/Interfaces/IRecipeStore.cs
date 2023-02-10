using CookBook.Common.Models;

namespace CookBook.Mobile.Services.Interfaces;

public interface IRecipeStore
{
	Task SaveRecipe(RecipeDetailModel recipe);

	Task<IEnumerable<RecipeDetailModel>> GetAllRecipes();

	Task<RecipeDetailModel?> GetRecipe(Guid id);
}