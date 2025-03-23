using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.Endpoints;

public static class RecipeEndpoints
{
    const string BasePath = "/api/recipes";
    const string BaseName = "Recipe";

    public static void UseRecipeEndpoints(this WebApplication app)
    {
        var RecipesTag = $"{BaseName}s";

        app.MapGet($"{BasePath}", (IRecipeFacade recipeFacade) => recipeFacade.GetAll())
            .WithTags(RecipesTag)
            .WithName($"Get{BaseName}sAll");

        app.MapGet($"{BasePath}/{{id:guid}}", (Guid id, IRecipeFacade recipeFacade) => recipeFacade.GetById(id))
            .WithTags(RecipesTag)
            .WithName($"Get{BaseName}ById");

        app.MapPost($"{BasePath}", (RecipeDetailModel recipe, IRecipeFacade recipeFacade) => recipeFacade.Create(recipe))
            .WithTags(RecipesTag)
            .WithName($"Create{BaseName}");

        app.MapPut($"{BasePath}", (RecipeDetailModel recipe, IRecipeFacade recipeFacade) => recipeFacade.Update(recipe))
            .WithTags(RecipesTag)
            .WithName($"Update{BaseName}");

        app.MapDelete($"{BasePath}/{{id:guid}}", (Guid id, IRecipeFacade recipeFacade) => recipeFacade.Delete(id))
            .WithTags(RecipesTag)
            .WithName($"Delete{BaseName}");
    }
}
