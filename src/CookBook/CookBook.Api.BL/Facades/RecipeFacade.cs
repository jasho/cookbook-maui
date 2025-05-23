﻿using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.BL.Mappers;
using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Repositories.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class RecipeFacade(
    IRecipeRepository recipeRepository,
    RecipeMapper recipeMapper)
    : IRecipeFacade
{
    public List<RecipeListModel> GetAll()
    {
        var recipeEntities = recipeRepository.GetAll();
        return recipeMapper.EntitiesToListModels(recipeEntities);
    }

    public RecipeDetailModel? GetById(Guid id)
    {
        var recipeEntity = recipeRepository.GetById(id);
        return recipeEntity is null
            ? null
            : recipeMapper.EntityToDetailModel(recipeEntity);
    }

    public Guid Create(RecipeDetailModel recipeModel)
    {
        var mergedRecipeModel = MergeIngredientAmounts(recipeModel);
        var recipeEntity = recipeMapper.DetailModelToEntity(mergedRecipeModel);
        return recipeRepository.Insert(recipeEntity);
    }

    public Guid? Update(RecipeDetailModel recipeModel)
    {
        var mergedRecipeModel = MergeIngredientAmounts(recipeModel);

        var recipeEntity = recipeMapper.DetailModelToEntity(mergedRecipeModel);
        recipeEntity.IngredientAmounts = mergedRecipeModel.IngredientAmounts?.Select(t =>
                                             new IngredientAmountEntity
                                             {
                                                 Id = t.Id ?? Guid.NewGuid(),
                                                 Amount = t.Amount,
                                                 Unit = t.Unit,
                                                 RecipeId = recipeEntity.Id,
                                                 IngredientId = t.Ingredient?.Id
                                             }).ToList()
                                         ?? [];

        var result = recipeRepository.Update(recipeEntity);
        return result;
    }

    private static RecipeDetailModel MergeIngredientAmounts(RecipeDetailModel recipe)
    {
        var result = new List<IngredientAmountModel>();
        var ingredientAmountGroups = recipe.IngredientAmounts?.GroupBy(t => $"{t.Ingredient?.Id}-{t.Unit}");

        if (ingredientAmountGroups is not null)
        {
            foreach (var ingredientAmountGroup in ingredientAmountGroups)
            {
                var ingredientAmountFirst = ingredientAmountGroup.First();
                var totalAmount = ingredientAmountGroup.Sum(t => t.Amount);
                var ingredientAmount = new IngredientAmountModel()
                {
                    Id = ingredientAmountFirst.Id,
                    Amount = totalAmount,
                    Unit = ingredientAmountFirst.Unit,
                    Ingredient = ingredientAmountFirst.Ingredient,
                };

                result.Add(ingredientAmount);
            }
        }

        return new RecipeDetailModel(recipe)
        {
            IngredientAmounts = result
        };
    }

    public void Delete(Guid id)
    {
        recipeRepository.Remove(id);
    }
}