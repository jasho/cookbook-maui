using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.App.Api;
using CookBook.Common.Models;

namespace CookBook.App.ViewModels;

public partial class RecipeListViewModel : ViewModelBase, IViewModel
{
    private readonly IRecipesClient recipesClient;

    [ObservableProperty]
    private ICollection<RecipeListModel>? items;

    public RecipeListViewModel(
        IRecipesClient recipesClient,
        INavigationService navigationService)
        : base(navigationService)
    {
        this.recipesClient = recipesClient;
    }

    public async Task OnAppearingAsync()
    {
        Items = await recipesClient.GetRecipesAllAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<RecipeDetailViewModel>(new Dictionary<string, object>
        {
            [nameof(RecipeDetailViewModel.Id)] = id
        });
    }

    [RelayCommand]
    private Task GoToCreateAsync()
        => Task.CompletedTask;
}