using CommunityToolkit.Maui;
using CookBook.Mobile.ViewModels;

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

            builder.Services.AddTransient<IngredientListViewModel>();


            return builder.Build();
        }
    }
}
