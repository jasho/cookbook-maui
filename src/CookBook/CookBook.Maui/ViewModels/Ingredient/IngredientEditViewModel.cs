using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Ingredient;

[QueryProperty(nameof(Ingredient), nameof(Ingredient))]
public partial class IngredientEditViewModel(IIngredientsClient ingredientsClient)
    : ViewModelBase
{
    public FileResult? ImageFileResult { get; set; }
    public IngredientDetailModel? Ingredient { get; init; }

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
                [DevicePlatform.Android] = [".png", ".jpg", ".jpeg"],
                [DevicePlatform.iOS] = [".png", ".jpg", ".jpeg"],
                [DevicePlatform.WinUI] = [".png", ".jpg", ".jpeg"]
            })
        });
    }
}
