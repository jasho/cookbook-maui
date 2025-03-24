using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.Endpoints;

public static class IngredientEndpoints
{
    const string BasePath = "/api/ingredients";
    const string BaseName = "Ingredient";

    public static void UseIngredientEndpoints(this WebApplication app)
    {
        var IngredientsTag = $"{BaseName}s";

        app.MapGet($"{BasePath}", (IIngredientFacade ingredientFacade) => ingredientFacade.GetAll())
            .WithTags(IngredientsTag)
            .WithName($"Get{BaseName}sAll");

        app.MapGet($"{BasePath}/{{id:guid}}", (Guid id, IIngredientFacade ingredientFacade) => ingredientFacade.GetById(id))
            .WithTags(IngredientsTag)
            .WithName($"Get{BaseName}ById");

        app.MapPost($"{BasePath}", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade) => ingredientFacade.Create(ingredient))
            .WithTags(IngredientsTag)
            .WithName($"Create{BaseName}");

        app.MapPut($"{BasePath}", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade) => ingredientFacade.Update(ingredient))
            .WithTags(IngredientsTag)
            .WithName($"Update{BaseName}");

        app.MapDelete($"{BasePath}/{{id:guid}}", (Guid id, IIngredientFacade ingredientFacade) => ingredientFacade.Delete(id))
            .WithTags(IngredientsTag)
            .WithName($"Delete{BaseName}");
    }
}
