using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.Endpoints;

public static class IngredientEndpoints
{
    public static void UseIngredientEndpoints(this WebApplication app)
    {
        const string IngredientsBasePath = "/api/ingredients";
        const string IngredientBaseName = "Ingredient";
        var IngredientsTag = $"{IngredientBaseName}s";

        app.MapGet($"{IngredientsBasePath}", (IIngredientFacade ingredientFacade) => ingredientFacade.GetAll())
            .WithTags(IngredientsTag)
            .WithName($"Get{IngredientBaseName}sAll");

        app.MapGet($"{IngredientsBasePath}/{{id:guid}}", (Guid id, IIngredientFacade ingredientFacade) => ingredientFacade.GetById(id))
            .WithTags(IngredientsTag)
            .WithName($"Get{IngredientBaseName}ById");

        app.MapPost($"{IngredientsBasePath}", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade) => ingredientFacade.Create(ingredient))
            .WithTags(IngredientsTag)
            .WithName($"Create{IngredientBaseName}");

        app.MapPut($"{IngredientsBasePath}", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade) => ingredientFacade.Update(ingredient))
            .WithTags(IngredientsTag)
            .WithName($"Update{IngredientBaseName}");

        app.MapDelete($"{IngredientsBasePath}/{{id:guid}}", (Guid id, IIngredientFacade ingredientFacade) => ingredientFacade.Delete(id))
            .WithTags(IngredientsTag)
            .WithName($"Delete{IngredientBaseName}");
    }
}
