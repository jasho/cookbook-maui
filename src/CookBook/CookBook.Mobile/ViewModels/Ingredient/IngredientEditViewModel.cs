using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Mobile.Api;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class IngredientEditViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;
    public Guid Id { get; set; } = Guid.Empty;

    public IngredientDetailModel Ingredient { get; set; }
    public FileResult? ImageFileResult { get; set; }

    public IngredientEditViewModel(
        IIngredientsClient ingredientsClient)
    {
        this.ingredientsClient = ingredientsClient;
    }

    public override async Task OnAppearingAsync()
    {
        Ingredient = await ingredientsClient.GetIngredientByIdAsync(Id);
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
        ImageFileResult = await MediaPicker.Default.CapturePhotoAsync();
        if (ImageFileResult is not null)
        {
            Ingredient.ImageUrl = ImageFileResult.FullPath;
        }
    }
}
