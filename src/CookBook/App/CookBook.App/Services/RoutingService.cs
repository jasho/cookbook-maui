﻿using CookBook.App.Components.Common.Models;
using CookBook.App.Components.Common.Services;
using CookBook.App.Components.Common.ViewModels;
using CookBook.App.Components.Views;
using CookBook.App.Views;

namespace CookBook.App.Services;

public class RoutingService : IRoutingService
{
    private static ICollection<RouteModel> routesCommon = new List<RouteModel>
    {
        new("//recipes", typeof(RecipeListView), typeof(RecipeListViewModel)),
        new("//ingredients", typeof(IngredientListView), typeof(IngredientListViewModel)),
        new("//settings", typeof(SettingsView), typeof(SettingsViewModel)),

        new("//ingredients/detail", typeof(IngredientDetailView), typeof(IngredientDetailViewModel)),
    };

    private static IEnumerable<RouteModel> routesPhone = new List<RouteModel>
    {
        new("//ingredients/edit", typeof(IngredientEditViewPhone), typeof(IngredientEditViewModel)),
        new("//ingredients/detail/edit", typeof(IngredientEditViewPhone), typeof(IngredientEditViewModel)),

        new("//recipes/detail", typeof(RecipeDetailViewPhone), typeof(RecipeDetailViewModel)),
    }.Concat(routesCommon);

    private static IEnumerable<RouteModel> routesDesktop = new List<RouteModel>
    {
        new("//ingredients/edit", typeof(IngredientEditViewDesktop), typeof(IngredientEditViewModel)),
        new("//ingredients/detail/edit", typeof(IngredientEditViewDesktop), typeof(IngredientEditViewModel)),

        new("//recipes/detail", typeof(RecipeDetailViewDesktop), typeof(RecipeDetailViewModel)),

        new("//recipes/detail/edit", typeof(RecipeEditViewDesktop), typeof(RecipeEditViewModel)),
        new("//recipes/edit", typeof(RecipeEditViewDesktop), typeof(RecipeEditViewModel)),

        new("//recipes/detail/edit/ingredients", typeof(RecipeIngredientsEditViewDesktop), typeof(RecipeIngredientsEditViewModel)),
        new("//recipes/edit/ingredients", typeof(RecipeIngredientsEditViewDesktop), typeof(RecipeIngredientsEditViewModel)),
    }.Concat(routesCommon);

    public IEnumerable<RouteModel> Routes
        => DeviceInfo.Idiom == DeviceIdiom.Phone
            ? routesPhone
            : routesDesktop;

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
