using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Models;
using CookBook.Maui.Messages;
using CookBook.Maui.Services;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Recipe;

public partial class RecipeListViewModel : ViewModelBase, IRecipient<RecipeCreatedMessage>, IRecipient<RecipeUpdatedMessage>
{
    private readonly IRecipesClient recipesClient;

    public RecipeListViewModel(IRecipesClient recipesClient)
    {
        this.recipesClient = recipesClient;

        IsActive = true;
    }

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
        await Shell.Current.GoToAsync(RoutingService.RecipeDetailRouteRelative,
            new Dictionary<string, object>
            {
                [nameof(RecipeDetailViewModel.Id)] = id
            });
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await Shell.Current.GoToAsync(RoutingService.RecipeEditRouteRelative);
    }

    public void Receive(RecipeCreatedMessage message)
    {
        ForceDataRefresh = true;
    }

    public void Receive(RecipeUpdatedMessage message)
    {
        ForceDataRefresh = true;
    }
}