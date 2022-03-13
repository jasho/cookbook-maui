using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;

namespace CookBook.Mobile.ViewModels.Recipe;

[QueryProperty(nameof(Id), "id")]
public class RecipeDetailViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;

    public string? Id { private get; set; }
    
    public RecipeDetailModel? Recipe { get; set; }

    public RecipeDetailViewModel(
        IRecipesClient recipesClient,
        ICommandFactory commandFactory)
    {
        this.recipesClient = recipesClient;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        if (Guid.TryParse(Id, out var id))
        {
            Recipe = await recipesClient.GetRecipeByIdAsync(id);
        }
    }
}
