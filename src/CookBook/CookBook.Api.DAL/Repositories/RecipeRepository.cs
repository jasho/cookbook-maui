using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Mappers;
using CookBook.Api.DAL.Repositories.Interfaces;

namespace CookBook.Api.DAL.Repositories;

public class RecipeRepository(
    RecipeMapper recipeMapper,
    IStorage storage)
    : IRecipeRepository
{
    public IList<RecipeEntity> GetAll()
    {
        return storage.Recipes;
    }

    public RecipeEntity? GetById(Guid id)
    {
        var recipeEntity = storage.Recipes.SingleOrDefault(recipe => recipe.Id == id);

        if (recipeEntity is not null)
        {
            recipeEntity.IngredientAmounts = GetIngredientAmountsByRecipeId(id);
            foreach (var ingredientAmount in recipeEntity.IngredientAmounts)
            {
                ingredientAmount.Ingredient = storage.Ingredients.SingleOrDefault(ingredientEntity => ingredientEntity.Id == ingredientAmount.IngredientId);
            }
        }

        return recipeEntity;
    }

    public Guid Insert(RecipeEntity entity)
    {
        storage.Recipes.Add(entity);

        foreach (var ingredientAmount in entity.IngredientAmounts)
        {
            var ingredientAmountEntity = ingredientAmount with { RecipeId = entity.Id };
            storage.IngredientAmounts.Add(ingredientAmountEntity);
        }

        return entity.Id;
    }

    public Guid? Update(RecipeEntity entity)
    {
        var entityExisting = storage.Recipes.SingleOrDefault(recipeEntity => recipeEntity.Id == entity.Id);

        if (entityExisting is not null)
        {
            recipeMapper.EntityToEntity(entity, entityExisting);
            entityExisting.IngredientAmounts = GetIngredientAmountsByRecipeId(entity.Id);
            //TODO: update ingredient amounts for existing recipe
            return entityExisting.Id;
        }
        else
        {
            return null;
        }
    }

    private IList<IngredientAmountEntity> GetIngredientAmountsByRecipeId(Guid recipeId)
    {
        return storage.IngredientAmounts.Where(ingredientAmountEntity => ingredientAmountEntity.RecipeId == recipeId).ToList();
    }

    public void Remove(Guid id)
    {
        var ingredientAmountsToRemove = storage.IngredientAmounts.Where(ingredientAmount => ingredientAmount.RecipeId == id).ToList();

        for (var i = 0; i < ingredientAmountsToRemove.Count; i++)
        {
            var ingredientAmountToRemove = ingredientAmountsToRemove.ElementAt(i);
            storage.IngredientAmounts.Remove(ingredientAmountToRemove);
        }

        var recipeToRemove = storage.Recipes.SingleOrDefault(recipeEntity => recipeEntity.Id == id);
        if (recipeToRemove is not null)
        {
            storage.Recipes.Remove(recipeToRemove);
        }
    }

    public bool Exists(Guid id)
    {
        return storage.Recipes.Any(recipe => recipe.Id == id);
    }
}