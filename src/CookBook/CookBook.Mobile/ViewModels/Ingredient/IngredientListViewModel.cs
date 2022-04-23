using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Services;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels;

public class IngredientListViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;
    private readonly IIngredientsClient ingredientsClient;

    public ICollection<IngredientListModel>? Items { get; set; }
    
    public ICommand GoToDetailCommand { get; set; }
    public ICommand GoToCreateCommand { get; set; }
    public ICommand GoToEditCommand { get; set; }

    public IngredientListViewModel(
        ICommandFactory commandFactory,
        IRoutingService routingService,
        IIngredientsClient ingredientsClient)
    {
        this.routingService = routingService;
        this.ingredientsClient = ingredientsClient;

        GoToDetailCommand = commandFactory.CreateCommand<Guid>(GoToDetailAsync);
        GoToCreateCommand = commandFactory.CreateCommand(GoToCreateAsync);
        GoToEditCommand = commandFactory.CreateCommand<Guid>(GoToEditAsync);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        Items = await ingredientsClient.GetIngredientsAllAsync();
    }

    private async Task GoToDetailAsync(Guid id)
    {
        var route = routingService.GetRouteByViewModel<IngredientDetailViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
    }

    private async Task GoToCreateAsync()
    {
        var route = routingService.GetRouteByViewModel<IngredientEditViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    private async Task GoToEditAsync(Guid id)
    {
        var route = routingService.GetRouteByViewModel<IngredientEditViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
    }
}
