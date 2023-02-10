#if WINDOWS
using System.Windows;
#elif ANDROID
using Android.Widget;
using Application = Android.App.Application;
#endif
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Services.Interfaces;
using PropertyChanged;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), "id")]
public partial class RecipeDetailViewModel : ViewModelBase
{
	private readonly IRecipesClient recipesClient;
	private readonly IIngredientsClient ingredientsClient;
	private readonly IRecipeStore recipeStore;
	private readonly IShare share;

	[DoNotNotify]
	public Guid Id { get; set; }

	public RecipeDetailModel? Recipe { get; set; }

	public RecipeDetailViewModel(
		IRecipesClient recipesClient,
		IIngredientsClient ingredientsClient,
		IRecipeStore recipeStore,
		IShare share)
	{
		this.recipesClient = recipesClient;
		this.ingredientsClient = ingredientsClient;
		this.recipeStore = recipeStore;
		this.share = share;
	}

	public override async Task OnAppearingAsync()
	{
		Recipe = await recipesClient.GetRecipeByIdAsync(Id);
	}

	[RelayCommand]
	private async Task DeleteAsync()
	{
		await recipesClient.DeleteRecipeAsync(Recipe!.Id!.Value);
		Shell.Current.SendBackButtonPressed();
	}

	[RelayCommand]
	private async Task GoToEditAsync()
	{
		if (Recipe is not null)
		{
			await Shell.Current.GoToAsync("/edit", new Dictionary<string, object> { ["recipe"] = Recipe });
		}
	}

	[RelayCommand]
	private async Task ShareAsync()
	{
		if (Recipe?.Id is not null)
		{
			await share.RequestAsync(new ShareTextRequest
			{
				Title = Recipe.Name,
				Text = $@"{Recipe.Name}

{Recipe.Description}",
				Subject = $"CookBook - {Recipe.Name}",
				Uri = $"https://app-cookbook-maui-api.azurewebsites.net/api/recipes/{Recipe.Id.Value}"
			});
		}
	}

	[RelayCommand]
	private async Task SaveRecipeAsync()
	{
		if (Recipe is not null)
		{
			if (await recipeStore.GetRecipe(Recipe.Id!.Value) is not null)
			{
#if ANDROID
				Toast.MakeText(Application.Context, "TODO This recipe is already downloaded!", ToastLength.Short).Show();
#elif WINDOWS
				MessageBox.Show("TODO This recipe is already downloaded!");
#endif
				return;
			}
			List<IngredientDetailModel> ingredientDetails = (await ingredientsClient.GetIngredientByRecipeIdAsync(Recipe.Id!.Value)).ToList();
			await recipeStore.SaveRecipe(Recipe);
		}
	}
}
