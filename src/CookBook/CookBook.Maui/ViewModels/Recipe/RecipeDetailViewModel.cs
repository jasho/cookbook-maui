using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Maui.ViewModels.Base;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Recipe;

[QueryProperty(nameof(Id), "id")]
public partial class RecipeDetailViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;
    private readonly IShare share;

    [ObservableProperty]
    private Guid id;

    public RecipeDetailModel? Recipe { get; set; }

    public RecipeDetailViewModel(
        IRecipesClient recipesClient,
        IShare share)
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
        Shell.Current.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Recipe is not null)
        {
            await Shell.Current.GoToAsync("/edit", new Dictionary<string, object> { ["recipe"] = Recipe });
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
