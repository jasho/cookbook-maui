using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Mobile.Api;
using Microsoft.Toolkit.Mvvm.Input;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Recipe), "recipe")]
public partial class RecipeEditViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;

    public RecipeDetailModel Recipe { get; set; } = null!;

    public List<FoodType> FoodTypes { get; set; }

    public RecipeEditViewModel(IRecipesClient recipesClient)
    {
        this.recipesClient = recipesClient;

        FoodTypes = new List<FoodType>((FoodType[])Enum.GetValues(typeof(FoodType)));
    }

    [ICommand]
    private async Task GoToRecipeIngredientEditAsync()
    {
        await Shell.Current.GoToAsync("/ingredients", new Dictionary<string, object> { [nameof(RecipeIngredientsEditViewModel.Recipe)] = Recipe });
    }

    [ICommand]
    private async Task DeleteAsync()
    {
        if (Recipe.Id is not null)
        {
            await recipesClient.DeleteRecipeAsync(Recipe.Id.Value);
        }

        await Shell.Current.GoToAsync("../");
    }

    [ICommand]
    private async Task SaveAsync()
    {
        await recipesClient.UpdateRecipeAsync(Recipe);
        await Shell.Current.GoToAsync("../");
    }
}