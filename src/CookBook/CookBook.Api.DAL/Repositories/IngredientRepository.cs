using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Mappers;
using CookBook.Api.DAL.Repositories.Interfaces;

namespace CookBook.Api.DAL.Repositories;

public class IngredientRepository(
    IngredientMapper ingredientMapper,
    IStorage storage)
    : IIngredientRepository
{
    public IList<IngredientEntity> GetAll()
    {
        return storage.Ingredients;
    }

    public IngredientEntity? GetById(Guid id)
    {
        return storage.Ingredients.SingleOrDefault(entity => entity.Id == id);
    }

    public Guid Insert(IngredientEntity ingredient)
    {
        storage.Ingredients.Add(ingredient);
        return ingredient.Id;
    }

    public Guid? Update(IngredientEntity entity)
    {
        var entityExisting = storage.Ingredients.SingleOrDefault(ingredientInStorage =>
            ingredientInStorage.Id == entity.Id);
        if (entityExisting != null)
        {
            ingredientMapper.EntityToEntity(entity, entityExisting);
        }

        return entityExisting?.Id;
    }

    public void Remove(Guid id)
    {
        var ingredientAmountsToRemove =
            storage.IngredientAmounts.Where(ingredientAmount => ingredientAmount.IngredientId == id).ToList();

        for (var i = 0; i < ingredientAmountsToRemove.Count; i++)
        {
            var ingredientAmountToRemove = ingredientAmountsToRemove.ElementAt(i);
            storage.IngredientAmounts.Remove(ingredientAmountToRemove);
        }

        var ingredientToRemove = storage.Ingredients.Single(ingredient => ingredient.Id.Equals(id));
        storage.Ingredients.Remove(ingredientToRemove);
    }

    public bool Exists(Guid id)
    {
        return storage.Ingredients.Any(ingredient => ingredient.Id == id);
    }
}