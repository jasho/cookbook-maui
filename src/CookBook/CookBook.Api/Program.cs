using AutoMapper;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Installers;
using CookBook.Api.Facades;
using CookBook.Common.Models;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureOpenApiDocuments(builder.Services);
new ApiDALInstaller().Install(builder.Services);
new ApiBLInstaller().Install(builder.Services);
builder.Services.AddAutoMapper(typeof(ApiBLInstaller));

var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);
UseSecurityFeatures(app);
UseRouting(app);
UseOpenApi(app);

app.Run();


void ConfigureOpenApiDocuments(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddOpenApiDocument(document =>
    {
        document.Title = "CookBook API";
        document.DocumentName = "cookbook-api";
    });
}

void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

void UseSecurityFeatures(IApplicationBuilder app)
{
    app.UseHttpsRedirection();
}

void UseRouting(WebApplication app)
{
    app.MapGet("/", async http => http.Response.Redirect("/swagger", false));

    UseIngredientRouting(app);
    UseRecipeRouting(app);
}

void UseIngredientRouting(WebApplication app)
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

void UseRecipeRouting(WebApplication app)
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

void UseOpenApi(IApplicationBuilder app)
{
    app.UseOpenApi();
    app.UseSwaggerUi3(settings =>
    {
        settings.DocumentTitle = "CookBook Swagger UI";
        settings.SwaggerRoutes.Add(new SwaggerUi3Route("CookBook API", "/swagger/cookbook-api/swagger.json"));
    });
}