using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.App.Components.Common.Api;
using CookBook.App.Components.Common.Services;
using CookBook.Common.Models;

namespace CookBook.App.Components.Common.ViewModels;

[QueryProperty(nameof(Id), "id")]
public partial class RecipeDetailViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;
    private readonly IShare share;
     
    public Guid Id { get; set; }

    [ObservableProperty]
    private RecipeDetailModel? recipe;

    public RecipeDetailViewModel(
        IRecipesClient recipesClient,
        IShare share,
        INavigationService navigationService)
        : base(navigationService)
    {
        this.recipesClient = recipesClient;
        this.share = share;
    }

    public override async Task OnAppearingAsync()
    {
        Recipe = await recipesClient.GetRecipeByIdAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        await recipesClient.DeleteRecipeAsync(Recipe!.Id!.Value);
        navigationService.GoBack();
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Recipe is not null)
        {
            await navigationService.GoToAsync("/edit", new Dictionary<string, object> { ["recipe"] = Recipe });
        }
    }

    [RelayCommand]
    private async Task ShareAsync()
    {
        if (Recipe?.Id is not null)
        {
            await share.RequestAsync(new ShareTextRequest
            {
                Title = Recipe.Name,
                Text = $@"{Recipe.Name}

{Recipe.Description}",
                Subject = $"CookBook - {Recipe.Name}",
                Uri = $"https://app-cookbook-maui-api.azurewebsites.net/api/recipes/{Recipe.Id.Value}"
            });
        }
    }
}
