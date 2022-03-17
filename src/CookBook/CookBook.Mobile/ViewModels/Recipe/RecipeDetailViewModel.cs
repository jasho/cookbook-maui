using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels.Recipe;

[QueryProperty(nameof(Id), "id")]
public class RecipeDetailViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;

    public string? Id { private get; set; }
    
    public RecipeDetailModel? Recipe { get; set; }

    public ICommand DeleteCommand { get; set; }
    public ICommand GoToEditCommand { get; set; }

    public RecipeDetailViewModel(
        IRecipesClient recipesClient,
        ICommandFactory commandFactory)
    {
        this.recipesClient = recipesClient;

        DeleteCommand = commandFactory.CreateCommand(DeleteAsync);
        GoToEditCommand = commandFactory.CreateCommand(GoToEditAsync);
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
}
