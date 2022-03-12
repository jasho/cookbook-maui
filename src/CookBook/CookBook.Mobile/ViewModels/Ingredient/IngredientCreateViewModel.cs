using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Services;
using System.Net.Sockets;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels.Ingredient;

public class IngredientCreateViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;
    private readonly IDeviceOrientationService deviceOrientationService;

    public FileResult? ImageFileResult { get; set; }

    public ICommand PickImageCommand { get; set; }
    public ICommand SaveCommand { get; set; }

    public IngredientDetailModel? Ingredient { get; set; } = new(Guid.NewGuid(), string.Empty, string.Empty, null);

    public IngredientCreateViewModel(
        IIngredientsClient ingredientsClient,
        ICommandFactory commandFactory,
        IDeviceOrientationService deviceOrientationService)
    {
        this.ingredientsClient = ingredientsClient;
        this.deviceOrientationService = deviceOrientationService;

        PickImageCommand = commandFactory.CreateCommand(PickImageAsync);
        SaveCommand = commandFactory.CreateCommand(SaveAsync);
    }

    public async Task SaveAsync()
    {
        var orientation = deviceOrientationService.GetOrientation();
        await ingredientsClient.CreateIngredientAsync(Ingredient);
        Shell.Current.SendBackButtonPressed();
    }

    private async Task PickImageAsync()
    {
        //#if WINDOWS
        //        var listener = new TcpListener(8080);
        //        listener.Start();

        //        //var socketListener = new Windows.Networking.Sockets.StreamSocketListener();
        //        //socketListener.BindServiceNameAsync("8080");
        //#endif

        ImageFileResult = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick an image",
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                [DevicePlatform.Android] = new[] { ".png", ".jpg", ".jpeg" },
                [DevicePlatform.iOS] = new[] { ".png", ".jpg", ".jpeg" },
                [DevicePlatform.UWP] = new[] { ".png", ".jpg", ".jpeg" }
            })
        });
    }
}
