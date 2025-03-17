using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Models;
using CookBook.Maui.Messages;
using CookBook.Maui.Services.Interfaces;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Recipe;

public partial class RecipeListViewModel : ViewModelBase, IRecipient<RecipeCreatedMessage>, IRecipient<RecipeUpdatedMessage>
{
    private readonly IRoutingService routingService;
    private readonly IRecipesClient recipesClient;

    /// <inheritdoc/>
    public RecipeListViewModel(IRoutingService routingService,
        IRecipesClient recipesClient)
    {
        this.routingService = routingService;
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
        var route = routingService.GetRouteByViewModel<RecipeDetailViewModel>();

        await Shell.Current.GoToAsync($"{route}", new Dictionary<string, object> { ["id"] = id });
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        var route = routingService.GetRouteByViewModel<RecipeEditViewModel>();
        await Shell.Current.GoToAsync($"{route}");
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