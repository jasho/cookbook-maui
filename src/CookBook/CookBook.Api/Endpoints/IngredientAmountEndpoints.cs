using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Api.Endpoints;

public static class IngredientAmountEndpoints
{
    const string BasePath = "/api/ingredient-amounts";
    const string BaseName = "IngredientAmount";

    public static void UseIngredientAmountEndpoints(this WebApplication app)
    {
        var IngredientAmountsTag = $"{BaseName}s";

        app.MapPut($"{BasePath}",
            (IIngredientAmountFacade ingredientAmountFacade, Guid recipeId, [FromBody] List<IngredientAmountModel> ingredientAmounts)
            => ingredientAmountFacade.UpdateIngredientAmounts(recipeId, ingredientAmounts))
            .WithTags(IngredientAmountsTag)
            .WithName($"Update{BaseName}s");
    }
}
