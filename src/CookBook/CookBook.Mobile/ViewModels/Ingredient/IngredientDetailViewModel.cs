using CookBook.Common.Models;
using CookBook.Mobile.Api;
using Microsoft.Toolkit.Mvvm.Input;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class IngredientDetailViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;
    private readonly IShare share;

    public Guid Id { get; set; } = Guid.Empty;

    public IngredientDetailModel? Ingredient { get; set; }

    public IngredientDetailViewModel(
        IIngredientsClient ingredientsClient,
        IShare share)
    {
        this.ingredientsClient = ingredientsClient;
        this.share = share;
    }

    public override async Task OnAppearingAsync()
    {
        Ingredient = await ingredientsClient.GetIngredientByIdAsync(Id);
    }

    [ICommand]
    private async Task DeleteAsync()
    {
        await ingredientsClient.DeleteIngredientAsync(Id);
        Shell.Current.SendBackButtonPressed();
    }

    [ICommand]
    private async Task GoToEditAsync()
    {
        await Shell.Current.GoToAsync($"/edit?id={Ingredient!.Id!.Value}");
    }

    [ICommand]
    private async Task ShareAsync()
    {
        if (Ingredient?.Id is not null)
        {
            await share.RequestAsync(new ShareTextRequest
            {
                Title = Ingredient.Name,
                Text = $@"{Ingredient.Name}

{Ingredient.Description}",
                Subject = $"CookBook - {Ingredient.Name}",
                Uri = $"https://app-cookbook-maui-api.azurewebsites.net/api/ingredients/{Ingredient.Id.Value}"
            });
        }
    }
}