using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Models;
using CookBook.Maui.Facades;
using CookBook.Maui.Messages;

namespace CookBook.Maui.ViewModels.Ingredient;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class IngredientEditViewModel(
    IIngredientsFacade ingredientsFacade,
    IMessenger messenger)
    : ViewModelBase
{
    public FileResult? ImageFileResult { get; set; }
    public Guid Id { get; init; } = Guid.Empty;

    [ObservableProperty]
    public partial IngredientDetailModel? Ingredient { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (Id == Guid.Empty)
        {
            Ingredient = new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                ImageUrl = null
            };
        }
        else
        {
            Ingredient = await ingredientsFacade.GetByIdAsync(Id);
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Ingredient is not null)
        {
            var ingredientId = await ingredientsFacade.CreateOrUpdateAsync(Ingredient);
            messenger.Send(new IngredientCreatedOrUpdatedMessage
            {
                IngredientId = ingredientId
            });
        }

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
