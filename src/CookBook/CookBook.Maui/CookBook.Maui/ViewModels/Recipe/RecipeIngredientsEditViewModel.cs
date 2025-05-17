using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Mobile.Api;
using System.Collections.ObjectModel;

namespace CookBook.Maui.ViewModels.Recipe
{
    [QueryProperty(nameof(RecipeId), nameof(RecipeId))]
    public partial class RecipeIngredientsEditViewModel(
        IIngredientsClient ingredientsClient,
        IRecipesClient recipesClient)
        : ViewModelBase
    {
        public Guid RecipeId { get; init; }

        [ObservableProperty]
        public partial RecipeDetailModel Recipe { get; set; } = new();

        public List<Unit> Units { get; set; } = [.. Enum.GetValues<Unit>()];
        public ObservableCollection<IngredientListModel> Ingredients { get; set; } = [];
        public IngredientAmountModel IngredientNew { get; private set; } = new();

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            if (RecipeId != Guid.Empty)
            {
                Recipe = await recipesClient.GetRecipeByIdAsync(RecipeId);
            }

            var ingredients = await ingredientsClient.GetIngredientsAllAsync();
            Ingredients.Clear();
            foreach (var ingredient in ingredients)
            {
                Ingredients.Add(ingredient);
            }

            IngredientNew = GetNewIngredient();
        }

        private IngredientAmountModel GetNewIngredient()
            => new()
            {
                Id = Guid.NewGuid(),
                Ingredient = Ingredients.FirstOrDefault()
            };

        [RelayCommand]
        private async Task AddNewIngredientToRecipeAsync()
        {
            if (Recipe.Id is not null)
            {
                Recipe.IngredientAmounts?.Add(IngredientNew);

                IngredientNew = GetNewIngredient();

                await recipesClient.UpdateRecipeAsync(Recipe);
            }
        }

        [RelayCommand]
        private async Task UpdateIngredientAsync(IngredientAmountModel ingredient)
        {
            // TODO: update individual item here instead of the whole recipe
            if (Recipe.Id is not null)
            {
                await recipesClient.UpdateRecipeAsync(Recipe);
            }
        }

        [RelayCommand]
        private async Task RemoveIngredientAsync(IngredientAmountModel ingredient)
        {
            // TODO: update individual item here instead of the whole recipe
            Recipe.IngredientAmounts?.Remove(ingredient);

            if (Recipe.Id is not null)
            {
                await recipesClient.UpdateRecipeAsync(Recipe);
            }
        }
    }
}