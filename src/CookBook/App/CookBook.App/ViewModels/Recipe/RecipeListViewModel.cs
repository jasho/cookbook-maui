﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.App.Api;
using CookBook.Common.Models;

namespace CookBook.App.ViewModels;

public partial class RecipeListViewModel : ObservableObject, IViewModel
{
    private readonly IRoutingService routingService;
    private readonly IRecipesClient recipesClient;

    [ObservableProperty]
    private ICollection<RecipeListModel>? items;

    public RecipeListViewModel(
        IRoutingService routingService,
        IRecipesClient recipesClient)
    {
        this.routingService = routingService;
        this.recipesClient = recipesClient;
    }

    public async Task OnAppearingAsync()
    {
        Items = await recipesClient.GetRecipesAllAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        var route = routingService.GetRouteByViewModel<RecipeDetailViewModel>();

        await Shell.Current.GoToAsync($"{route}", new Dictionary<string, object> { ["id"] = id });
    }

    [RelayCommand]
    private Task GoToCreateAsync()
        => Task.CompletedTask;
}