using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Models;
using CookBook.Maui.Facades.Interfaces;
using CookBook.Maui.Messages;
using CookBook.Maui.Services;

namespace CookBook.Maui.ViewModels.Recipe;

public partial class RecipeListViewModel
    : ViewModelBase, IRecipient<RecipeCreatedOrUpdatedMessage>
{
    private readonly IRecipesFacade recipesFacade;

    public RecipeListViewModel(IRecipesFacade recipesFacade)
    {
        this.recipesFacade = recipesFacade;
     
        IsActive = true;
    }

    [ObservableProperty]
    public partial ICollection<RecipeListModel>? Items { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Items = await recipesFacade.GetAllItemsAsync();
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

    public void Receive(RecipeCreatedOrUpdatedMessage message)
    {
        ForceDataRefresh = true;
    }
}