using CookBook.Common.Models;
using CookBook.Mobile.Api;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), "id")]
[INotifyPropertyChanged]
public partial class RecipeDetailViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;
    private readonly IShare share;

    [ObservableProperty]
    private string? id;

    [ObservableProperty]
    private RecipeDetailModel? recipe;

    public RecipeDetailViewModel(
        IRecipesClient recipesClient,
        IShare share)
    {
        this.recipesClient = recipesClient;
        this.share = share;
    }

    public override async Task OnAppearingAsync()
    {
        if (Guid.TryParse(Id, out var id))
        {
            Recipe = await recipesClient.GetRecipeByIdAsync(id);
        }
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
        await Shell.Current.GoToAsync($"/edit?id={Recipe!.Id!.Value}");
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
