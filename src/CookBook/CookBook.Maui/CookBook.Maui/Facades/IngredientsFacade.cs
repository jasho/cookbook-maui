using CookBook.Common.Models;
using CookBook.Maui.Entities;
using CookBook.Maui.Mappers;
using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.Api;

namespace CookBook.Maui.Facades;

public class IngredientsFacade(
    IDatabaseService databaseService,
    IIngredientsClient ingredientsClient,
    IConnectivity connectivity,
    IngredientMapper ingredientMapper,
    FacadeBase<IngredientListModel, IngredientDetailModel>.Dependencies dependencies)
    : FacadeBase<IngredientListModel, IngredientDetailModel>(dependencies), IIngredientsFacade
{
    protected override async Task<ICollection<IngredientListModel>> GetAllItemsOnlineAsync()
        => await ingredientsClient.GetIngredientsAllAsync();

    protected override async Task<ICollection<IngredientListModel>> GetAllItemsLocalAsync()
    {
        var ingredientEntities = await databaseService.GetAllAsync<IngredientEntity>();
        return ingredientMapper.EntitiesToListModels(ingredientEntities);
    }

    protected override async Task<IngredientDetailModel?> GetByIdOnlineAsync(Guid id)
        => await ingredientsClient.GetIngredientByIdAsync(id);

    protected override async Task<IngredientDetailModel?> GetByIdLocalAsync(Guid id)
    {
        var ingredientEntity = await databaseService.GetByIdAsync<IngredientEntity>(id);
        return ingredientMapper.EntityToDetailModel(ingredientEntity);
    }

    protected override async Task<Guid> CreateOrUpdateOnlineAsync(IngredientDetailModel detailModel)
    {
        if(detailModel.Id is null)
        {
            return await ingredientsClient.CreateIngredientAsync(detailModel);
        }
        else
        {
            var existingIngredient = ingredientsClient.GetIngredientByIdAsync(detailModel.Id.Value);
            if(existingIngredient is null)
            {
                return await ingredientsClient.CreateIngredientAsync(detailModel);
            }
            else
            {
                return (await ingredientsClient.UpdateIngredientAsync(detailModel)) ?? detailModel.Id.Value;
            }
        }
    }

    protected override async Task<Guid> CreateOrUpdateLocalAsync(IngredientDetailModel detailModel)
    {
        var ingredientEntity = ingredientMapper.DetailModelToEntity(detailModel);
        return await databaseService.CreateOrUpdateAsync(ingredientEntity);
    }

    protected override async Task DeleteOnlineAsync(Guid id)
        => await ingredientsClient.DeleteIngredientAsync(id);

    protected override async Task DeleteLocalAsync(Guid id)
    {
        await databaseService.DeleteAsync<IngredientEntity>(id);
    }
}