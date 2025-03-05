using AutoMapper;
using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Repositories.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class RecipeFacade(
    IMapper mapper,
    IRecipeRepository recipeRepository)
    : IRecipeFacade
{
    public List<RecipeListModel> GetAll()
    {
        var recipeEntities = recipeRepository.GetAll();
        return mapper.Map<List<RecipeListModel>>(recipeEntities);
    }

    public RecipeDetailModel? GetById(Guid id)
    {
        var recipeEntity = recipeRepository.GetById(id);
        return mapper.Map<RecipeDetailModel>(recipeEntity);
    }

    public Guid Create(RecipeDetailModel recipeModel)
    {
        var mergedRecipeModel = MergeIngredientAmounts(recipeModel);
        var recipeEntity = mapper.Map<RecipeEntity>(mergedRecipeModel);
        return recipeRepository.Insert(recipeEntity);
    }

    public Guid? Update(RecipeDetailModel recipeModel)
    {
        var mergedRecipeModel = MergeIngredientAmounts(recipeModel);

        var recipeEntity = mapper.Map<RecipeEntity>(mergedRecipeModel);
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
        var result = new List<RecipeDetailIngredientModel>();
        var ingredientAmountGroups = recipe.IngredientAmounts?.GroupBy(t => $"{t.Ingredient?.Id}-{t.Unit}");

        if (ingredientAmountGroups is not null)
        {
            foreach (var ingredientAmountGroup in ingredientAmountGroups)
            {
                var ingredientAmountFirst = ingredientAmountGroup.First();
                var totalAmount = ingredientAmountGroup.Sum(t => t.Amount);
                var ingredientAmount = new RecipeDetailIngredientModel()
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