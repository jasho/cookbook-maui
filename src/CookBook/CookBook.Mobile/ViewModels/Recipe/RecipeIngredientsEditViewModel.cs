using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Mobile.Api;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CookBook.Mobile.ViewModels
{
    [QueryProperty(nameof(Recipe), nameof(Recipe))]
    [QueryProperty(nameof(IsRefreshRequested), nameof(IsRefreshRequested))]
    public partial class RecipeIngredientsEditViewModel : ViewModelBase
    {
        private readonly IIngredientsClient ingredientsClient;
        private readonly IRecipesClient recipesClient;
        private RecipeDetailModel recipe;

        public bool IsRefreshRequested { get; set; } = true;

        public RecipeDetailModel Recipe
        {
            get => recipe;
            set
            {
                recipe = value;
                recipe.IngredientAmounts = new ObservableCollection<RecipeDetailIngredientModel>(recipe.IngredientAmounts);
            }
        }

        public List<Unit> Units { get; set; }
        public ObservableCollection<IngredientListModel> Ingredients { get; set; } = new();
        public RecipeDetailIngredientModel? IngredientNew { get; private set; }

        public RecipeIngredientsEditViewModel(
            IIngredientsClient ingredientsClient,
            IRecipesClient recipesClient)
        {
            this.ingredientsClient = ingredientsClient;
            this.recipesClient = recipesClient;

            Units = new List<Unit>((Unit[]) Enum.GetValues(typeof(Unit)));
        }

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
            => new(Guid.NewGuid(), 0, Unit.Unknown, Ingredients.FirstOrDefault());

        [ICommand]
        private async Task AddNewIngredientToRecipeAsync()
        {
            Recipe.IngredientAmounts.Add(IngredientNew);
            IngredientNew = GetNewIngredient();

            await recipesClient.UpdateRecipeAsync(Recipe);
        }

        [ICommand]
        private async Task UpdateIngredientAsync(RecipeDetailIngredientModel ingredient)
        {
            // TODO: update individual item here instead of the whole recipe
            await recipesClient.UpdateRecipeAsync(Recipe);
        }

        [ICommand]
        private async Task RemoveIngredientAsync(RecipeDetailIngredientModel ingredient)
        {
            // TODO: update individual item here instead of the whole recipe
            Recipe.IngredientAmounts.Remove(ingredient);

            await recipesClient.UpdateRecipeAsync(Recipe);
        }
    }
}