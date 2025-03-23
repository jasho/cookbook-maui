using CookBook.Common.Models;
using CookBook.Maui.Entities;
using CookBook.Maui.Mappers;
using CookBook.Maui.Services.Interfaces;
using CookBook.Mobile.Api;
using System.Net;

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

    protected override async Task<Guid> CreateOrUpdateOnlineAsync(IngredientDetailModel item)
    {
        if(item.Id is null)
        {
            return await ingredientsClient.CreateIngredientAsync(item);
        }
        else
        {
            var existingIngredient = ingredientsClient.GetIngredientByIdAsync(item.Id.Value);
            if(existingIngredient is null)
            {
                return await ingredientsClient.CreateIngredientAsync(item);
            }
            else
            {
                return (await ingredientsClient.UpdateIngredientAsync(item)) ?? item.Id.Value;
            }
        }
    }

    protected override async Task<Guid> CreateOrUpdateLocalAsync(IngredientDetailModel detailModel)
    {
        var ingredientEntity = ingredientMapper.DetailModelToEntity(detailModel);
        return await databaseService.CreateOrUpdateAsync(ingredientEntity);
    }
}