using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Maui.Options;
using CookBook.Maui.Pages;
using CookBook.Maui.Resources.Fonts;
using CookBook.Maui.Services;
using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.Shells;
using CookBook.Maui.ViewModels;
using CookBook.Maui.ViewModels.Ingredient;
using CookBook.Maui.ViewModels.Recipe;
using CookBook.Mobile.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CookBook.Maui;

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

        ConfigureShell(builder.Services);
        ConfigureViews(builder.Services);
        ConfigureViewModels(builder.Services);

        ConfigureServices(builder.Services);
        ConfigureApiClients(builder.Services);

        //ConfigureCustomHandlers(builder);

        var app = builder.Build();

        RegisterRoutes(app);

        return app;
    }

    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        var appSettingsFilePath = "CookBook.Maui.Configuration.appsettings.json";
        using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
        if (appSettingsStream is not null)
        {
            configurationBuilder.AddJsonStream(appSettingsStream);
        }

        var configuration = configurationBuilder.Build();
        builder.Configuration.AddConfiguration(configuration);
    }

    private static void ConfigureOptions(MauiAppBuilder builder)
    {
        builder.Services.Configure<ApiOptions>(options =>
            builder.Configuration.GetSection(nameof(ApiOptions)).Bind(options));
    }

    private static void ConfigureShell(IServiceCollection services)
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            services.AddSingleton<AppShellPhone>();
        }
        else
        {
            services.AddSingleton<AppShellDesktop>();
        }
    }

    private static void ConfigureViews(IServiceCollection services)
    {
        services.TryAddTransient<IngredientDetailPagePhone>();
        services.TryAddTransient<IngredientEditPagePhone>();
        services.TryAddTransient<IngredientListPage>();
        services.TryAddTransient<RecipeDetailPagePhone>();
        services.TryAddTransient<RecipeListPage>();
        services.TryAddTransient<SettingsPage>();
    }

    private static void ConfigureViewModels(IServiceCollection services)
    {
        services.TryAddTransient<IngredientDetailViewModel>();
        services.TryAddTransient<IngredientEditViewModel>();
        services.TryAddTransient<IngredientListViewModel>();
        services.TryAddTransient<RecipeDetailViewModel>();
        services.TryAddTransient<RecipeEditViewModel>();
        services.TryAddTransient<RecipeIngredientsEditViewModel>();
        services.TryAddTransient<RecipeListViewModel>();
        services.TryAddTransient<SettingsViewModel>();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IRoutingService, RoutingService>();
        services.AddSingleton<IShare>(_ => Share.Default);
        services.AddSingleton<IGlobalExceptionService, GlobalExceptionService>();
        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
        services.AddSingleton(Preferences.Default);
        services.AddSingleton<IPreferencesService, PreferencesService>();

        services.AddSingleton<ILanguageSelectorService, LanguageSelectorService>();
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
        //services.AddSingleton<IGlobalExceptionServiceInitializer, GlobalExceptionServiceInitializer>();
    }

    private static void ConfigureApiClients(IServiceCollection services)
    {
        services.AddHttpClient<IIngredientsClient, IngredientsClient>((serviceProvider, client) =>
        {
            var apiOptions = serviceProvider.GetRequiredService<IOptions<ApiOptions>>();
            client.BaseAddress = new Uri(apiOptions.Value.ApiUrl ?? throw new NullReferenceException($"Value for '{nameof(apiOptions)}.{nameof(ApiOptions.ApiUrl)}' cannot be null"));
        });

        services.AddHttpClient<IRecipesClient, RecipesClient>((serviceProvider, client) =>
        {
            var apiOptions = serviceProvider.GetRequiredService<IOptions<ApiOptions>>();
            client.BaseAddress = new Uri(apiOptions.Value.ApiUrl ?? throw new NullReferenceException($"Value for '{nameof(apiOptions)}.{nameof(ApiOptions.ApiUrl)}' cannot be null"));
        });
    }

    //        private static void ConfigureCustomHandlers(MauiAppBuilder builder)
    //        {
    //            builder.ConfigureMauiHandlers(handlers =>
    //            {
    //#if __ANDROID__
    //                handlers.AddHandler(typeof(CustomEntry), typeof(CustomEntryHandler));
    //#endif
    //            });
    //        }

    private static void RegisterRoutes(MauiApp app)
    {
        var routingService = app.Services.GetRequiredService<IRoutingService>();

        foreach (var routeModel in routingService.Routes)
        {
            Routing.RegisterRoute(routeModel.Route, routeModel.ViewType);
        }
    }
}