using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Services;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels.Ingredient;

[QueryProperty(nameof(Id), "id")]
public class IngredientDetailViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;
    private readonly IRoutingService routingService;

    public string? Id { private get; set; }

    public IngredientDetailModel? Ingredient { get; set; }

    public ICommand DeleteCommand { get; set; }
    public ICommand GoToEditCommand { get; set; }

    public IngredientDetailViewModel(
        IIngredientsClient ingredientsClient,
        IRoutingService routingService,
        ICommandFactory commandFactory)
    {
        this.ingredientsClient = ingredientsClient;
        this.routingService = routingService;

        DeleteCommand = commandFactory.CreateCommand(DeleteAsync);
        GoToEditCommand = commandFactory.CreateCommand(GoToEditAsync);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        if (Guid.TryParse(Id, out var id))
        {
            Ingredient = await ingredientsClient.GetIngredientByIdAsync(id);
        }
    }

    private async Task DeleteAsync()
    {
        await ingredientsClient.DeleteIngredientAsync(Ingredient!.Id!.Value);
        Shell.Current.SendBackButtonPressed();
    }

    private async Task GoToEditAsync()
    {
        await Shell.Current.GoToAsync($"/edit?id={Ingredient!.Id!.Value}");
    }
}