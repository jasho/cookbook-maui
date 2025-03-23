using CookBook.Common.Models;
using CookBook.Maui.Entities;
using CookBook.Maui.Mappers;
using CookBook.Maui.Services.Interfaces;
using CookBook.Mobile.Api;

namespace CookBook.Maui.Facades;

public class RecipesFacade(
    IRecipesClient recipesClient,
    IDatabaseService databaseService,
    RecipeMapper recipeMapper,
    FacadeBase<RecipeListModel, RecipeDetailModel>.Dependencies dependencies)
    : FacadeBase<RecipeListModel, RecipeDetailModel>(dependencies)
{
    protected override async Task<Guid> CreateOrUpdateLocalAsync(RecipeDetailModel detailModel)
    {
        var recipeEntity = recipeMapper.DetailModelToEntity(detailModel);
        return await databaseService.CreateOrUpdateAsync(recipeEntity);
    }

    protected override async Task<Guid> CreateOrUpdateOnlineAsync(RecipeDetailModel detailModel)
    {
        if (detailModel.Id is null)
        {
            return await recipesClient.CreateRecipeAsync(detailModel);
        }
        else
        {
            var existingIngredient = recipesClient.GetRecipeByIdAsync(detailModel.Id.Value);
            if (existingIngredient is null)
            {
                return await recipesClient.CreateRecipeAsync(detailModel);
            }
            else
            {
                return (await recipesClient.UpdateRecipeAsync(detailModel)) ?? detailModel.Id.Value;
            }
        }
    }

    protected override async Task DeleteLocalAsync(Guid id)
        => await databaseService.DeleteAsync<RecipeEntity>(id);

    protected override async Task DeleteOnlineAsync(Guid id)
        => await recipesClient.DeleteRecipeAsync(id);

    protected override async Task<ICollection<RecipeListModel>> GetAllItemsLocalAsync()
    {
        var recipeEntities = await databaseService.GetAllAsync<RecipeEntity>();
        return recipeMapper.EntitiesToListModels(recipeEntities);
    }

    protected override async Task<ICollection<RecipeListModel>> GetAllItemsOnlineAsync()
        => await recipesClient.GetRecipesAllAsync();

    protected override async Task<RecipeDetailModel?> GetByIdLocalAsync(Guid id)
    {
        var recipeEntity = await databaseService.GetByIdAsync<RecipeEntity>(id);
        return recipeMapper.EntityToDetailModel(recipeEntity);
    }

    protected override async Task<RecipeDetailModel?> GetByIdOnlineAsync(Guid id)
        => await recipesClient.GetRecipeByIdAsync(id);
}
