﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Mobile.Api;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class IngredientDetailViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;
    private readonly IShare share;

    public Guid Id { get; set; } = Guid.Empty;

    [ObservableProperty]
    private IngredientDetailModel? ingredient;

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

    [RelayCommand]
    private async Task DeleteAsync()
    {
        await ingredientsClient.DeleteIngredientAsync(Id);
        Shell.Current.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await Shell.Current.GoToAsync("/edit", new Dictionary<string, object>
        {
            [nameof(IngredientEditViewModel.Ingredient)] = Ingredient!
        });
    }

    [RelayCommand]
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