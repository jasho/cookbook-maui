using CommunityToolkit.Maui;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Resources.Fonts;
using CookBook.Mobile.Services;
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
                       fonts.AddFont("FontAwesome-Solid.ttf", Fonts.FontAwesome);
                       fonts.AddFont("Montserrat-Bold.ttf", Fonts.Bold);
                       fonts.AddFont("Montserrat-Medium.ttf", Fonts.Medium);
                       fonts.AddFont("Montserrat-Regular.ttf", Fonts.Regular);
                   });

            ConfigureViews(builder.Services);
            ConfigureViewModels(builder.Services);
            ConfigureServices(builder.Services);
            ConfigureApiClients(builder.Services);

            var app = builder.Build();

            RegisterRoutes(app);

            return app;
        }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.Scan(selector => selector
                .FromAssemblyOf<App>()
                .AddClasses(filter => filter.AssignableTo<ContentPageBase>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }

        private static void ConfigureViewModels(IServiceCollection services)
        {
            services.Scan(selector => selector
                .FromAssemblyOf<App>()
                .AddClasses(filter => filter.AssignableTo<IViewModel>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }
        
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICommandFactory, CommandFactory>();
            services.AddSingleton<IRoutingService, RoutingService>();
            services.AddSingleton<IDeviceOrientationService, DeviceOrientationService>();
            services.AddSingleton<IShare>(_ => Share.Current);
        }

        private static void ConfigureApiClients(IServiceCollection services)
        {
            var baseUri = new Uri("https://app-cookbook-maui-api.azurewebsites.net/");

            services.AddHttpClient<IIngredientsClient, IngredientsClient>((serviceProvider, client) =>
            {
                client.BaseAddress = baseUri;
            });

            services.AddHttpClient<IRecipesClient, RecipesClient>((serviceProvider, client) =>
            {
                client.BaseAddress = baseUri;
            });
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
