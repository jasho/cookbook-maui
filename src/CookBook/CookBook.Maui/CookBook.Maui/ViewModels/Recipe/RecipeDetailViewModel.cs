using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Models;
using CookBook.Maui.Facades.Interfaces;
using CookBook.Maui.Messages;
using CookBook.Maui.Services;

namespace CookBook.Maui.ViewModels.Recipe;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class RecipeDetailViewModel
    : ViewModelBase, IRecipient<RecipeCreatedOrUpdatedMessage>
{
    private readonly IRecipesFacade recipesFacade;
    private readonly IShare share;

    public RecipeDetailViewModel(
        IRecipesFacade recipesFacade,
        IShare share)
    {
        this.recipesFacade = recipesFacade;
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
            Recipe = await recipesFacade.GetByIdAsync(Id);
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Recipe?.Id is not null)
        {
            await recipesFacade.DeleteAsync(Recipe.Id.Value);
        }

        Shell.Current.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Recipe?.Id is not null)
        {
            await Shell.Current.GoToAsync(RoutingService.RecipeEditRouteRelative, new Dictionary<string, object>
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

    public void Receive(RecipeCreatedOrUpdatedMessage message)
    {
        if (message.RecipeId == Recipe?.Id)
        {
            ForceDataRefresh = true;
        }
    }
}
