using CommunityToolkit.Maui;
using CookBook.Mobile.Factories;
using CookBook.Mobile.ViewModels;
using CookBook.Mobile.Views;

namespace CookBook.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .UseMauiCommunityToolkit()
                   .ConfigureFonts(fonts =>
                   {
                       fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   });

            builder.Services.AddSingleton<ICommandFactory, CommandFactory>();

            builder.Services.AddTransient<IngredientListViewModel>();
            builder.Services.AddTransient<IngredientDetailViewModel>();

            Routing.RegisterRoute("ingredients/detail", typeof(IngredientDetailView));

            return builder.Build();
        }
    }
}
