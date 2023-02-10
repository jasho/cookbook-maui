using CookBook.Common.Models;
using CookBook.Entities;
using CookBook.Mobile.Services.Interfaces;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace CookBook.Mobile.Services;

class RecipeStore : IRecipeStore
{

	private SQLiteAsyncConnection? sql;

	public async Task SaveRecipe(RecipeDetailModel recipe)
	{
		await EnsureInitialized();
		var pk = recipe.Id;
		var recipeEntity = new RecipeEntity
		{
			Id = recipe.Id!.Value,
			Description = recipe.Description,
			Duration = recipe.Duration,
			FoodType = recipe.FoodType,
			Name = recipe.Name,
			ImageUrl = recipe.ImageUrl,
			IngredientAmounts = recipe.IngredientAmounts.Select(s => new IngredientAmountEntity
			{
				RecipeId = recipe.Id,
				Amount = s.Amount,
				IngredientId = s.Ingredient.Id,
				Unit = s.Unit,
				Id = s.Id!.Value
			}).ToList()
		};
		await sql.InsertWithChildrenAsync(recipeEntity, recursive: false);

		var res = await sql.GetWithChildrenAsync<RecipeEntity>(pk);
		Console.WriteLine(res.Duration);
	}

	public async Task<IEnumerable<RecipeDetailModel>> GetAllRecipes()
	{
		await EnsureInitialized();
		var children = await sql.GetAllWithChildrenAsync<RecipeEntity>();
		return children.Select(entity => new RecipeDetailModel
		{
			Id = entity.Id,
			Name = entity.Name,
			Description = entity.Description,
			Duration = entity.Duration,
			FoodType = entity.FoodType,
			ImageUrl = entity.ImageUrl,
			IngredientAmounts = entity.IngredientAmounts.Select(s => new RecipeDetailIngredientModel()
			{
				Id = s.Id,
				Amount = s.Amount,
				Unit = s.Unit,
				Ingredient = new IngredientListModel(s.Ingredient!.Id, s.Ingredient.Name, s.Ingredient.ImageUrl)
			}).ToList()
		});
	}

	public async Task<RecipeDetailModel?> GetRecipe(Guid id)
	{
		await EnsureInitialized();
		var entity = await sql.GetWithChildrenAsync<RecipeEntity>(id);
		return new RecipeDetailModel
		{
			Id = entity.Id,
			Name = entity.Name,
			Description = entity.Description,
			Duration = entity.Duration,
			FoodType = entity.FoodType,
			ImageUrl = entity.ImageUrl,
			IngredientAmounts = entity.IngredientAmounts.Select(s => new RecipeDetailIngredientModel()
			{
				Id = s.Id,
				Amount = s.Amount,
				Unit = s.Unit,
				Ingredient = new IngredientListModel(s.Ingredient!.Id, s.Ingredient.Name, s.Ingredient.ImageUrl)
			}).ToList()
		};
	}

	private async Task EnsureInitialized()
	{
		if (sql is not null)
		{
			return;
		}

		var flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;

		var domain = AppDomain.CurrentDomain.BaseDirectory;

#if WINDOWS
		sql = new SQLiteAsyncConnection(Path.Combine(domain, "localdb.db"), flags);
#elif ANDROID
		sql = new SQLiteAsyncConnection("localdb.db", flags);
#endif

		await sql.CreateTablesAsync<RecipeEntity, IngredientEntity, IngredientAmountEntity>();
	}
}