using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Models;
using CookBook.Maui.Messages;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Recipe;

[QueryProperty(nameof(Id), "id")]
public partial class RecipeDetailViewModel : ViewModelBase, IRecipient<RecipeUpdatedMessage>
{
    private readonly IRecipesClient recipesClient;
    private readonly IShare share;

    public RecipeDetailViewModel(
        IRecipesClient recipesClient,
        IShare share)
    {
        this.recipesClient = recipesClient;
        this.share = share;

        IsActive = true;
    }

    public Guid Id { get; init; } = Guid.Empty;

    [ObservableProperty]
    public partial RecipeDetailModel? Recipe { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (Id != Guid.Empty)
        {
            Recipe = await recipesClient.GetRecipeByIdAsync(Id);
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
    private async Task GoToEditAsync()
    {
        if (Recipe?.Id is not null)
        {
            await Shell.Current.GoToAsync("/edit", new Dictionary<string, object>
            {
                [nameof(RecipeEditViewModel.Id)] = Recipe.Id
            });
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
                Text = $"""
                        {Recipe.Name}

                        {Recipe.Description}
                        """,
                Subject = $"CookBook - {Recipe.Name}",
                Uri = $"https://app-cookbook-maui-api.azurewebsites.net/api/recipes/{Recipe.Id.Value}"
            });
        }
    }

    public void Receive(RecipeUpdatedMessage message)
    {
        if (message.RecipeId == Recipe?.Id)
        {
            ForceDataRefresh = true;
        }
    }
}
