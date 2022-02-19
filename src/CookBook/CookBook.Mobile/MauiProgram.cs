using CommunityToolkit.Maui;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Services;
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

            builder.Services.AddSingleton<ICommandFactory, CommandFactory>();
            builder.Services.AddSingleton<IRoutingService, RoutingService>();

            builder.Services.Scan(selector => selector
                .FromAssemblyOf<App>()
                .AddClasses(filter => filter.AssignableTo<IViewModel>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());

            var app = builder.Build();

            RegisterRoutes(app);

            return app;
        }

        private static void RegisterRoutes(MauiApp app)
        {
            var routingService = app.Services.GetRequiredService<IRoutingService>();

            foreach (var routeModel in routingService.Routes)
            {
                Routing.RegisterRoute(routeModel.Route, routeModel.ViewType);
            }
        }
    }
}
