using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Ingredient;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class IngredientDetailViewModel(
    IIngredientsClient ingredientsClient,
    IShare share)
    : ViewModelBase
{
    public Guid Id { get; init; } = Guid.Empty;

    [ObservableProperty]
    public partial IngredientDetailModel? Ingredient { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (Id != Guid.Empty)
        {
            Ingredient = await ingredientsClient.GetIngredientByIdAsync(Id);
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Ingredient?.Id is not null)
        {
            await ingredientsClient.DeleteIngredientAsync(Ingredient.Id.Value);
        }

        Shell.Current.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        Dictionary<string, object> navigationParameters = [];

        if (Ingredient?.Id is not null)
        {
            navigationParameters.Add(nameof(IngredientEditViewModel.Id), Ingredient.Id.Value);
        }

        await Shell.Current.GoToAsync("/edit", navigationParameters);
    }

    [RelayCommand]
    private async Task ShareAsync()
    {
        if (Ingredient?.Id is not null)
        {
            await share.RequestAsync(new ShareTextRequest
            {
                Title = Ingredient.Name,
                Text = $"""
                        {Ingredient.Name}

                        {Ingredient.Description}
                        """,
                Subject = $"CookBook - {Ingredient.Name}",
                Uri = $"https://app-cookbook-maui-api.azurewebsites.net/api/ingredients/{Ingredient.Id.Value}"
            });
        }
    }
}