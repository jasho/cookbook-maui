using CommunityToolkit.Maui;
using CookBook.App.Resources.Fonts;

namespace CookBook.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome-Solid.ttf", Fonts.FontAwesome);
                fonts.AddFont("Montserrat-Bold.ttf", Fonts.Bold);
                fonts.AddFont("Montserrat-Medium.ttf", Fonts.Medium);
                fonts.AddFont("Montserrat-Regular.ttf", Fonts.Regular);
            });

        return builder.Build();
    }
}