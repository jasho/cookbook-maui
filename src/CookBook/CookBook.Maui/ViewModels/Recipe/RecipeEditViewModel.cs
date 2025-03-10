using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Recipe;

[QueryProperty(nameof(Recipe), "recipe")]
public partial class RecipeEditViewModel(IRecipesClient recipesClient)
    : ViewModelBase
{
    public RecipeDetailModel Recipe { get; set; } = null!;

    public List<FoodType> FoodTypes { get; set; } = [..Enum.GetValues<FoodType>()];

    [RelayCommand]
    private async Task GoToRecipeIngredientEditAsync()
    {
        await Shell.Current.GoToAsync("/ingredients", new Dictionary<string, object> { [nameof(RecipeIngredientsEditViewModel.Recipe)] = Recipe });
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Recipe.Id is not null)
        {
            await recipesClient.DeleteRecipeAsync(Recipe.Id.Value);
        }

        await Shell.Current.GoToAsync("../");
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await recipesClient.UpdateRecipeAsync(Recipe);
        await Shell.Current.GoToAsync("../");
    }
}