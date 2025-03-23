using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Maui.Messages;
using CookBook.Maui.Services;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Recipe;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class RecipeEditViewModel(
    IRecipesClient recipesClient,
    IMessenger messenger)
    : ViewModelBase
{
    public Guid Id { get; init; } = Guid.Empty;

    [ObservableProperty]
    public partial RecipeDetailModel Recipe { get; set; } = new();

    public List<FoodType> FoodTypes { get; set; } = [.. Enum.GetValues<FoodType>()];

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (Id != Guid.Empty)
        {
            Recipe = await recipesClient.GetRecipeByIdAsync(Id);
        }
    }

    [RelayCommand]
    private async Task GoToRecipeIngredientEditAsync()
    {
        if (Recipe.Id is not null)
        {
            await Shell.Current.GoToAsync(RoutingService.RecipeIngredientsEditRouteRelative,
                new Dictionary<string, object>
                {
                    [nameof(RecipeIngredientsEditViewModel.RecipeId)] = Recipe.Id
                });
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Recipe.Id is not null)
        {
            await recipesClient.DeleteRecipeAsync(Recipe.Id.Value);
        }

        Shell.Current.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Recipe.Id is not null)
        {
            if (Id == Guid.Empty)
            {
                await recipesClient.CreateRecipeAsync(Recipe);

                messenger.Send<RecipeCreatedMessage>();
            }
            else
            {
                await recipesClient.UpdateRecipeAsync(Recipe);

                messenger.Send(new RecipeCreatedOrUpdatedMessage
                {
                    RecipeId = Recipe.Id.Value
                });
            }
        }

        Shell.Current.SendBackButtonPressed();
    }
}