using CookBook.Api.BL.Facades;
using CookBook.Api.BL.Facades.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api.BL.Installers;

public class ApiBLInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddSingleton<IIngredientFacade, IngredientFacade>();
        services.AddSingleton<IRecipeFacade, RecipeFacade>();
    }
}