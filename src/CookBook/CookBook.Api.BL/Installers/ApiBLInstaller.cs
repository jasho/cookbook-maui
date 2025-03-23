using CookBook.Api.BL.Facades;
using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.BL.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api.BL.Installers;

public class ApiBLInstaller
{
    public void Install(IServiceCollection services)
    {
		services.AddSingleton<IImageFacade, ImageFacade>();
		services.AddSingleton<IIngredientAmountFacade, IngredientAmountFacade>();
        services.AddSingleton<IIngredientFacade, IngredientFacade>();
        services.AddSingleton<IRecipeFacade, RecipeFacade>();

        services.AddSingleton<ImageMapper>();
        services.AddSingleton<IngredientAmountMapper>();
        services.AddSingleton<IngredientMapper>();
        services.AddSingleton<RecipeMapper>();
    }
}