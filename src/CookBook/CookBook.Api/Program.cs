using System.Net;
using System.Text.Json.Serialization;
using AutoMapper;
using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Installers;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Http.Json;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureOpenApiDocuments(builder.Services);
new ApiDALInstaller().Install(builder.Services);
new ApiBLInstaller().Install(builder.Services);
builder.Services.AddAutoMapper(typeof(ApiBLInstaller));
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var application = builder.Build();

ValidateAutoMapperConfiguration(application.Services);
UseSecurityFeatures(application);
UseRouting(application);
UseOpenApi(application);

application.Run();


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
    app.MapGet("/", http =>
    {
        http.Response.Redirect("/swagger", false);
        return Task.CompletedTask;
    });

    UseIngredientRouting(app);
    UseRecipeRouting(app);
    UseImageRouting(app);
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

void UseImageRouting(WebApplication app)
{
    const string ImageBasePath = "/api/images";
    const string ImageBaseName = "Image";
    var ImagesTag = $"{ImageBaseName}s";

    app.MapGet($"{ImageBasePath}/{{id:guid}}", (Guid id, IImageFacade imageFacade) => Results.File(imageFacade.GetById(id)?.Data ?? [], "image/**"))
        .WithTags(ImagesTag)
        .WithName($"Get{ImageBaseName}ById");

    app.MapPost($"{ImageBasePath}", (ImageModel image, HttpRequest request, IImageFacade imageFacade) =>
        {
            // TODO input validation maybe ;)
            var id = imageFacade.Create(new ImageModel { Id = image.Id, Data = image.Data });
            return Results.Created($"{request.Scheme}://{request.Host}{request.Path}/{id}", $"{request.Scheme}://{request.Host}{request.Path}/{id}");
        })
        .WithTags(ImagesTag)
        .WithName($"Create{ImageBaseName}")
        .Produces<string>((int)HttpStatusCode.Created);

    app.MapPut($"{ImageBasePath}", (ImageModel image, IImageFacade imageFacade) =>
        {
            var id = imageFacade.Update(image);
            return id is null
                ? Results.NotFound()
                : Results.NoContent();
        })
        .WithTags(ImagesTag)
        .WithName($"Update{ImageBaseName}");

    app.MapDelete($"{ImageBasePath}/{{id:guid}}", (Guid id, IImageFacade imageFacade) => imageFacade.Delete(id))
        .WithTags(ImagesTag)
        .WithName($"Delete{ImageBaseName}");
}

void UseOpenApi(IApplicationBuilder app)
{
    app.UseOpenApi();
    app.UseSwaggerUi(settings =>
    {
        settings.DocumentTitle = "CookBook Swagger UI";
        settings.SwaggerRoutes.Add(new SwaggerUiRoute("CookBook API", "/swagger/cookbook-api/swagger.json"));
        settings.ValidateSpecification = true;
    });
}