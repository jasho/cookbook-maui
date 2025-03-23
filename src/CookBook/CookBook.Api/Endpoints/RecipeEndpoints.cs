using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.Endpoints;

public static class RecipeEndpoints
{
    public static void UseRecipeEndpoints(this WebApplication app)
    {
        const string RecipeBasePath = "/api/recipes";
        const string RecipeBaseName = "Recipe";
        var RecipesTag = $"{RecipeBaseName}s";

        app.MapGet($"{RecipeBasePath}", (IRecipeFacade recipeFacade) => recipeFacade.GetAll())
            .WithTags(RecipesTag)
            .WithName($"Get{RecipeBaseName}sAll");

        app.MapGet($"{RecipeBasePath}/{{id:guid}}", (Guid id, IRecipeFacade recipeFacade) => recipeFacade.GetById(id))
            .WithTags(RecipesTag)
            .WithName($"Get{RecipeBaseName}ById");

        app.MapPost($"{RecipeBasePath}", (RecipeDetailModel recipe, IRecipeFacade recipeFacade) => recipeFacade.Create(recipe))
            .WithTags(RecipesTag)
            .WithName($"Create{RecipeBaseName}");

        app.MapPut($"{RecipeBasePath}", (RecipeDetailModel recipe, IRecipeFacade recipeFacade) => recipeFacade.Update(recipe))
            .WithTags(RecipesTag)
            .WithName($"Update{RecipeBaseName}");

        app.MapDelete($"{RecipeBasePath}/{{id:guid}}", (Guid id, IRecipeFacade recipeFacade) => recipeFacade.Delete(id))
            .WithTags(RecipesTag)
            .WithName($"Delete{RecipeBaseName}");
    }
}
