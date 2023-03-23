﻿using CommunityToolkit.Mvvm.Input;
using CookBook.App.Api;
using CookBook.Common.Models;

namespace CookBook.App.ViewModels;

[QueryProperty(nameof(Ingredient), nameof(Ingredient))]
public partial class IngredientEditViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;

    public FileResult? ImageFileResult { get; set; }
    public IngredientDetailModel Ingredient { get; init; }

    public IngredientEditViewModel(
        IIngredientsClient ingredientsClient,
        INavigationService navigationService)
        : base(navigationService)
    {
        this.ingredientsClient = ingredientsClient;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await ingredientsClient.CreateIngredientAsync(Ingredient);
        navigationService.GoBack();
    }

    [RelayCommand]
    private async Task PickImageAsync()
    {
        ImageFileResult = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick an image",
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                [DevicePlatform.Android] = new[] { ".png", ".jpg", ".jpeg" },
                [DevicePlatform.iOS] = new[] { ".png", ".jpg", ".jpeg" },
                [DevicePlatform.WinUI] = new[] { ".png", ".jpg", ".jpeg" }
            })
        });
    }
}
