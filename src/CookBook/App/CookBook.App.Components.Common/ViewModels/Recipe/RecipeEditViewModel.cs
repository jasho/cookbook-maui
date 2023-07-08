using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.App.Components.Common.Api;
using CookBook.App.Components.Common.Services;
using CookBook.Common.Enums;
using CookBook.Common.Models;

namespace CookBook.App.Components.Common.ViewModels;

[QueryProperty(nameof(Recipe), "recipe")]
public partial class RecipeEditViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;

    [ObservableProperty]
    private RecipeDetailModel recipe = null!;

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
        await navigationService.GoToAsync("/ingredients", new Dictionary<string, object> { [nameof(RecipeIngredientsEditViewModel.Recipe)] = Recipe });
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Recipe.Id is not null)
        {
            await recipesClient.DeleteRecipeAsync(Recipe.Id.Value);
        }

        await navigationService.GoToAsync("../");
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await recipesClient.UpdateRecipeAsync(Recipe);
        await navigationService.GoToAsync("../");
    }
}