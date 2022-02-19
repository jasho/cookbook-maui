using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels.Ingredient;

public class IngredientListViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;
    private readonly IIngredientsClient ingredientsClient;

    public ICollection<IngredientListModel> Ingredients { get; set; }

    public ICommand GoToDetailCommand { get; set; }

    public IngredientListViewModel(
        ICommandFactory commandFactory,
        IRoutingService routingService,
        IIngredientsClient ingredientsClient)
    {
        GoToDetailCommand = commandFactory.CreateCommand<Guid>(GoToDetailAsync);
        this.routingService = routingService;
        this.ingredientsClient = ingredientsClient;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        Ingredients = await ingredientsClient.GetIngredientsAllAsync();
    }

    private async Task GoToDetailAsync(Guid id)
    {
        var route = routingService.GetRouteByViewModel<IngredientDetailViewModel>();
        await Shell.Current.GoToAsync($"//{route}?id={id}");
    }
}
