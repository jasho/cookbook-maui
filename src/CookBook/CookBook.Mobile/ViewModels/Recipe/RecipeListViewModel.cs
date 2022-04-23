using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Services;
using Microsoft.Toolkit.Mvvm.Input;

namespace CookBook.Mobile.ViewModels;

public partial class RecipeListViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;
    private readonly IRecipesClient recipesClient;

    public ICollection<RecipeListModel>? Items { get; set; }

    public RecipeListViewModel(
        IRoutingService routingService,
        ICommandFactory commandFactory,
        IRecipesClient recipesClient)
    {
        this.routingService = routingService;
        this.recipesClient = recipesClient;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        Items = await recipesClient.GetRecipesAllAsync();
    }

    [ICommand]
    private async Task GoToDetailAsync(Guid id)
    {
        var route = routingService.GetRouteByViewModel<RecipeDetailViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
    }

    [ICommand]
    private async Task GoToCreateAsync()
    {
    }
}