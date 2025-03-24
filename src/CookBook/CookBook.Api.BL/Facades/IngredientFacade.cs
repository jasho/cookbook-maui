using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.DAL.Repositories.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class IngredientFacade(
    IIngredientRepository ingredientRepository,
    IngredientMapper ingredientMapper)
    : IIngredientFacade
{
    public List<IngredientListModel> GetAll()
    {
        var ingredientEntities = ingredientRepository.GetAll();
        return ingredientMapper.EntitiesToListModels(ingredientEntities);
    }

    public IngredientDetailModel? GetById(Guid id)
    {
        var ingredientEntity = ingredientRepository.GetById(id);
        return ingredientEntity is null
            ? null
            : ingredientMapper.EntityToDetailModel(ingredientEntity);
    }

    public Guid Create(IngredientDetailModel ingredientModel)
    {
        var ingredientEntity = ingredientMapper.DetailModelToEntity(ingredientModel);
        return ingredientRepository.Insert(ingredientEntity);
    }

    public Guid? Update(IngredientDetailModel ingredientModel)
    {
        var ingredientEntity = ingredientMapper.DetailModelToEntity(ingredientModel);
        return ingredientRepository.Update(ingredientEntity);
    }

    public void Delete(Guid id)
    {
        ingredientRepository.Remove(id);
    }
}