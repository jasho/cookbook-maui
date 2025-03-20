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
    IngredientMapper ingredientMapper)
    : IIngredientsFacade
{
    public async Task<ICollection<IngredientListModel>> GetIngredientsAllAsync()
    {
        var ingredientEntities = await databaseService.GetAllAsync<IngredientEntity>();
        var ingredients = ingredientMapper.IngredientsToIngredientListModels(ingredientEntities);

        if (connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            var ingredientsRemote = await ingredientsClient.GetIngredientsAllAsync();
            ingredients.AddRange(ingredientsRemote);
        }

        return ingredients;
    }

    public async Task<IngredientDetailModel?> GetIngredientByIdAsync(Guid id)
    {
        if (connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                return await ingredientsClient.GetIngredientByIdAsync(id);
            }
            catch (ApiException e) when(e.StatusCode == (int) HttpStatusCode.OK)
            {
                return await GetLocalIngredientByIdAsync(id);
            }
        }
        else
        {
            return await GetLocalIngredientByIdAsync(id);
        }
    }

    private async Task<IngredientDetailModel?> GetLocalIngredientByIdAsync(Guid id)
    {
        var ingredientEntity = await databaseService.GetByIdAsync<IngredientEntity>(id);
        return ingredientMapper.IngredientToIngredientDetailModel(ingredientEntity);
    }
}