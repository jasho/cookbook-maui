using CommunityToolkit.Mvvm.Input;
using CookBook.App.Api;
using CookBook.Common.Enums;
using CookBook.Common.Models;

namespace CookBook.App.ViewModels;

[QueryProperty(nameof(Recipe), "recipe")]
public partial class RecipeEditViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;

    public RecipeDetailModel Recipe { get; set; } = null!;

    public List<FoodType> FoodTypes { get; set; }

    public RecipeEditViewModel(
        IRecipesClient recipesClient,
        INavigationService navigationService)
        : base(navigationService)
    {
        this.recipesClient = recipesClient;

        FoodTypes = new List<FoodType>((FoodType[])Enum.GetValues(typeof(FoodType)));
    }

    [RelayCommand]
    private async Task GoToRecipeIngredientEditAsync()
    {
        await navigationService.GoToAsync("/ingredients", new Dictionary<string, object>
        {
            [nameof(RecipeIngredientsEditViewModel.Recipe)] = Recipe
        });
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Recipe.Id is not null)
        {
            await recipesClient.DeleteRecipeAsync(Recipe.Id.Value);
        }

        navigationService.GoBack();
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await recipesClient.UpdateRecipeAsync(Recipe);
        navigationService.GoBack();
    }
}