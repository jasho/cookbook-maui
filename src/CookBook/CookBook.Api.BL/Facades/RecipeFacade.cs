using AutoMapper;
using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Repositories.Interfaces;
using CookBook.Common.Enums;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class RecipeFacade : IRecipeFacade
{
    private readonly IMapper mapper;
    private readonly IRecipeRepository recipeRepository;

    public RecipeFacade(
        IMapper mapper,
        IRecipeRepository recipeRepository)
    {
        this.mapper = mapper;
        this.recipeRepository = recipeRepository;
    }

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
                                                 Unit = t.Unit ?? Unit.Unknown,
                                                 RecipeId = recipeEntity.Id,
                                                 IngredientId = t.Ingredient?.Id
                                             }).ToList()
                                         ?? new List<IngredientAmountEntity>();

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

        return new RecipeDetailModel
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Duration = recipe.Duration,
            FoodType = recipe.FoodType,
            IngredientAmounts = result,
            ImageUrl = recipe.ImageUrl,
        };
    }

    public void Delete(Guid id)
    {
        recipeRepository.Remove(id);
    }
}