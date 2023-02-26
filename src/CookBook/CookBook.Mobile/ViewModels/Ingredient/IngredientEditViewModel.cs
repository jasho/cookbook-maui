using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Mobile.Api;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Ingredient), nameof(Ingredient))]
public partial class IngredientEditViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;

    public FileResult? ImageFileResult { get; set; }

    [ObservableProperty]
    private IngredientDetailModel ingredient = new();

    public IngredientEditViewModel(
        IIngredientsClient ingredientsClient)
    {
        this.ingredientsClient = ingredientsClient;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await ingredientsClient.CreateIngredientAsync(Ingredient);
        Shell.Current.SendBackButtonPressed();
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
