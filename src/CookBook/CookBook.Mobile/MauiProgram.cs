﻿using CommunityToolkit.Maui;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Options;
using CookBook.Mobile.Resources.Fonts;
using CookBook.Mobile.Services;
using CookBook.Mobile.ViewModels;
using CookBook.Mobile.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

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

            ConfigureAppSettings(builder);
            ConfigureOptions(builder);
            
            ConfigureViews(builder.Services);
            ConfigureViewModels(builder.Services);
            
            ConfigureServices(builder.Services);
            ConfigureApiClients(builder.Services);

            var app = builder.Build();

            RegisterRoutes(app);

            return app;
        }

        private static void ConfigureAppSettings(MauiAppBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder();
            
            var assembly = Assembly.GetExecutingAssembly();
            var appSettingsFilePath = "CookBook.Mobile.Configuration.appsettings.json";
            using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
            configurationBuilder.AddJsonStream(appSettingsStream);
            
            var configuration = configurationBuilder.Build();
            builder.Configuration.AddConfiguration(configuration);
        }

        private static void ConfigureOptions(MauiAppBuilder builder)
        {
            builder.Services.Configure<ApiOptions>(options =>
                builder.Configuration.GetSection(nameof(ApiOptions)).Bind(options));
        }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.Scan(selector => selector
                .FromAssemblyOf<App>()
                .AddClasses(filter => filter.AssignableTo<ContentPageBase>())
                    .AsSelf()
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
            services.AddHttpClient<IIngredientsClient, IngredientsClient>((serviceProvider, client) =>
            {
                var apiOptions = serviceProvider.GetRequiredService<IOptions<ApiOptions>>();
                
                client.BaseAddress = new Uri(apiOptions.Value.ApiUrl);
            });

            services.AddHttpClient<IRecipesClient, RecipesClient>((serviceProvider, client) =>
            {
                var apiOptions = serviceProvider.GetRequiredService<IOptions<ApiOptions>>();

                client.BaseAddress = new Uri(apiOptions.Value.ApiUrl);
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
