using CookBook.Api.Facades;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api.BL.Installers
{
    public class ApiBLInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddSingleton<IIngredientFacade, IngredientFacade>();
            services.AddSingleton<IRecipeFacade, RecipeFacade>();
        }
    }
}
