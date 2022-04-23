using CookBook.Common.Models;
using CookBook.Mobile.Api;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), "id")]
[INotifyPropertyChanged]
public partial class IngredientDetailViewModel : IViewModel
{
    private readonly IIngredientsClient ingredientsClient;
    private readonly IShare share;

    [ObservableProperty]
    private string? id;

    [ObservableProperty]
    private IngredientDetailModel? ingredient;

    public IngredientDetailViewModel(
        IIngredientsClient ingredientsClient,
        IShare share)
    {
        this.ingredientsClient = ingredientsClient;
        this.share = share;
    }

    public async Task OnAppearingAsync()
    {
        if (Guid.TryParse(Id, out var id))
        {
            Ingredient = await ingredientsClient.GetIngredientByIdAsync(id);
        }
    }

    [ICommand]
    private async Task DeleteAsync()
    {
        await ingredientsClient.DeleteIngredientAsync(Ingredient!.Id!.Value);
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