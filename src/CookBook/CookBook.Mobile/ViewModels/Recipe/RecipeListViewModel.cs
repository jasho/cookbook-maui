using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CookBook.Mobile.ViewModels;

[INotifyPropertyChanged]
public partial class RecipeListViewModel : IViewModel
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
#if WINDOWS
        foreach (var item in Items)
	    {
            item.ImageUrl = item.ImageUrl?.Split('/').Last();
	    }
#endif
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