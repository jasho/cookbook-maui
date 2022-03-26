using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels.Recipe;

[QueryProperty(nameof(Id), "id")]
public class RecipeDetailViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;
    private readonly IShare share;

    public string? Id { private get; set; }
    
    public RecipeDetailModel? Recipe { get; set; }

    public ICommand DeleteCommand { get; set; }
    public ICommand GoToEditCommand { get; set; }
    public ICommand ShareCommand { get; set; }

    public RecipeDetailViewModel(
        IRecipesClient recipesClient,
        ICommandFactory commandFactory,
        IShare share)
    {
        this.recipesClient = recipesClient;
        this.share = share;
        DeleteCommand = commandFactory.CreateCommand(DeleteAsync);
        GoToEditCommand = commandFactory.CreateCommand(GoToEditAsync);
        ShareCommand = commandFactory.CreateCommand(ShareAsync);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        if (Guid.TryParse(Id, out var id))
        {
            Recipe = await recipesClient.GetRecipeByIdAsync(id);
        }
    }

    private async Task DeleteAsync()
    {
        await recipesClient.DeleteRecipeAsync(Recipe!.Id!.Value);
        Shell.Current.SendBackButtonPressed();
    }

    private async Task GoToEditAsync()
    {
        await Shell.Current.GoToAsync($"/edit?id={Recipe!.Id!.Value}");
    }

    private async Task ShareAsync()
    {
        if(Recipe is not null)
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
