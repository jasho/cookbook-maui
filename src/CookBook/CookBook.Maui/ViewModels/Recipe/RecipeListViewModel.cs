using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Maui.Services.Interfaces;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Recipe;

public partial class RecipeListViewModel(
    IRoutingService routingService,
    IRecipesClient recipesClient)
    : ViewModelBase
{
    [ObservableProperty]
    public partial ICollection<RecipeListModel>? Items { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

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