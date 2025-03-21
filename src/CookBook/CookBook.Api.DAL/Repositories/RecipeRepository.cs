﻿using AutoMapper;
using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Repositories.Interfaces;

namespace CookBook.Api.DAL.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly IMapper mapper;
    private readonly IStorage storage;

    public RecipeRepository(
        IMapper mapper,
        IStorage storage)
    {
        this.mapper = mapper;
        this.storage = storage;
    }

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
        var recipeEntityExisting = storage.Recipes.SingleOrDefault(recipeEntity => recipeEntity.Id == entity.Id);

        if (recipeEntityExisting is not null)
        {
            mapper.Map(entity, recipeEntityExisting);
            recipeEntityExisting.IngredientAmounts = GetIngredientAmountsByRecipeId(entity.Id);
            //TODO: update ingredient amounts for existing recipe
            return recipeEntityExisting.Id;
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