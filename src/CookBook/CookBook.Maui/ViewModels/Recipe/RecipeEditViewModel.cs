using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Recipe;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class RecipeEditViewModel(IRecipesClient recipesClient)
    : ViewModelBase
{
    public Guid Id { get; init; } = Guid.Empty;

    [ObservableProperty]
    public partial RecipeDetailModel? Recipe { get; set; }

    public List<FoodType> FoodTypes { get; set; } = [.. Enum.GetValues<FoodType>()];

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Recipe = Id == Guid.Empty
            ? new RecipeDetailModel()
            : await recipesClient.GetRecipeByIdAsync(Id);
    }

    [RelayCommand]
    private async Task GoToRecipeIngredientEditAsync()
    {
        if (Recipe?.Id is not null)
        {
            await Shell.Current.GoToAsync("/ingredients", new Dictionary<string, object>
            {
                [nameof(RecipeIngredientsEditViewModel.RecipeId)] = Recipe.Id
            });
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Recipe?.Id is not null)
        {
            await recipesClient.DeleteRecipeAsync(Recipe.Id.Value);
        }

        Shell.Current.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Recipe is not null)
        {
            if (Id == Guid.Empty)
            {
                await recipesClient.CreateRecipeAsync(Recipe);

            }
            else
            {
                await recipesClient.UpdateRecipeAsync(Recipe);
            }
        }

        Shell.Current.SendBackButtonPressed();
    }
}