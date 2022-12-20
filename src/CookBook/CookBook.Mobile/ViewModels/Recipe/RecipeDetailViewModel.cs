using CookBook.Common.Models;
using CookBook.Mobile.Api;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), "id")]
public partial class RecipeDetailViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;
    private readonly IShare share;

    [DoNotNotify]
    public Guid Id { get; set; }

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

    [ICommand]
    private async Task DeleteAsync()
    {
        await recipesClient.DeleteRecipeAsync(Recipe!.Id!.Value);
        Shell.Current.SendBackButtonPressed();
    }

    [ICommand]
    private async Task GoToEditAsync()
    {
        if (Recipe is not null)
        {
            await Shell.Current.GoToAsync("/edit", new Dictionary<string, object> { ["recipe"] = Recipe });
        }
    }

    [ICommand]
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
