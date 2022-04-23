using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), "id")]
public class IngredientEditViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;

    public FileResult? ImageFileResult { get; set; }
    public string? Id { private get; set; }

    public ICommand PickImageCommand { get; set; }
    public ICommand SaveCommand { get; set; }

    public IngredientDetailModel? Ingredient { get; set; }

    public IngredientEditViewModel(
        IIngredientsClient ingredientsClient,
        ICommandFactory commandFactory)
    {
        this.ingredientsClient = ingredientsClient;

        PickImageCommand = commandFactory.CreateCommand(PickImageAsync);
        SaveCommand = commandFactory.CreateCommand(SaveAsync);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        if (Id is not null && Guid.TryParse(Id, out var id))
        {
            Ingredient = await ingredientsClient.GetIngredientByIdAsync(id);
        }
        else
        {
            Ingredient = new(Guid.NewGuid(), string.Empty, string.Empty, null);
        }
    }

    public async Task SaveAsync()
    {
        await ingredientsClient.CreateIngredientAsync(Ingredient);
        Shell.Current.SendBackButtonPressed();
    }

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
