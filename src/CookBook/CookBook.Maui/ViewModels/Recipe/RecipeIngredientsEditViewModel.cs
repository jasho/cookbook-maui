﻿using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Mobile.Api;
using System.Collections.ObjectModel;

namespace CookBook.Maui.ViewModels.Recipe
{
    [QueryProperty(nameof(Recipe), nameof(Recipe))]
    [QueryProperty(nameof(IsRefreshRequested), nameof(IsRefreshRequested))]
    public partial class RecipeIngredientsEditViewModel(
        IIngredientsClient ingredientsClient,
        IRecipesClient recipesClient)
        : ViewModelBase
    {
        public bool IsRefreshRequested { get; set; } = true;

        public RecipeDetailModel Recipe { get; set; } = null!;

        public List<Unit> Units { get; set; } = [..Enum.GetValues<Unit>()];
        public ObservableCollection<IngredientListModel> Ingredients { get; set; } = [];
        public RecipeDetailIngredientModel? IngredientNew { get; private set; }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();

            if (IsRefreshRequested)
            {
                var ingredients = await ingredientsClient.GetIngredientsAllAsync();

                Ingredients.Clear();
                foreach (var ingredient in ingredients)
                {
                    Ingredients.Add(ingredient);
                }

                IngredientNew = GetNewIngredient();

                IsRefreshRequested = false;
            }
        }

        private RecipeDetailIngredientModel GetNewIngredient()
            => new()
            {
                Id = Guid.NewGuid(),
                Ingredient = Ingredients.FirstOrDefault()
            };

        [RelayCommand]
        private async Task AddNewIngredientToRecipeAsync()
        {
            if (IngredientNew is not null)
            {
                Recipe.IngredientAmounts?.Add(IngredientNew);
            }

            IngredientNew = GetNewIngredient();

            await recipesClient.UpdateRecipeAsync(Recipe);
        }

        [RelayCommand]
        private async Task UpdateIngredientAsync(RecipeDetailIngredientModel ingredient)
        {
            // TODO: update individual item here instead of the whole recipe
            await recipesClient.UpdateRecipeAsync(Recipe);
        }

        [RelayCommand]
        private async Task RemoveIngredientAsync(RecipeDetailIngredientModel ingredient)
        {
            // TODO: update individual item here instead of the whole recipe
            Recipe.IngredientAmounts?.Remove(ingredient);

            await recipesClient.UpdateRecipeAsync(Recipe);
        }
    }
}