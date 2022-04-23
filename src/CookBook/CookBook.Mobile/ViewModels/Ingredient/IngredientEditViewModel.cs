using CookBook.Common.Models;
using CookBook.Mobile.Api;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), "id")]
[INotifyPropertyChanged]
public partial class IngredientEditViewModel : IViewModel
{
    private readonly IIngredientsClient ingredientsClient;

    [ObservableProperty]
    private FileResult? imageFileResult;

    [ObservableProperty]
    private string? id;

    [ObservableProperty]
    private IngredientDetailModel? ingredient;

    public IngredientEditViewModel(
        IIngredientsClient ingredientsClient)
    {
        this.ingredientsClient = ingredientsClient;
    }

    public async Task OnAppearingAsync()
    {
        if (Id is not null && Guid.TryParse(Id, out var id))
        {
            Ingredient = await ingredientsClient.GetIngredientByIdAsync(id);
        }
        else
        {
            Ingredient = new(Guid.NewGuid(), string.Empty, string.Empty, null);
        }
    }

    [ICommand]
    private async Task SaveAsync()
    {
        await ingredientsClient.CreateIngredientAsync(Ingredient);
        Shell.Current.SendBackButtonPressed();
    }

    [ICommand]
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
