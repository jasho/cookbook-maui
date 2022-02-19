using CookBook.Common.Models;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels.Ingredient;

public class IngredientListViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;

    public ObservableCollection<IngredientListModel> Ingredients { get; set; } = new ObservableCollection<IngredientListModel>();

    public ICommand GoToDetailCommand { get; set; }

    public IngredientListViewModel(
        ICommandFactory commandFactory,
        IRoutingService routingService)
    {
        GoToDetailCommand = commandFactory.CreateCommand<Guid>(GoToDetailAsync);
        this.routingService = routingService;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        Ingredients.Clear();
        Ingredients.Add(new IngredientListModel(
            Guid.NewGuid(),
            "Vejce",
            "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Chicken_egg_2009-06-04.jpg/428px-Chicken_egg_2009-06-04.jpg"));
    }

    private async Task GoToDetailAsync(Guid id)
    {
        var route = routingService.GetRouteByViewModel<IngredientDetailViewModel>();
        await Shell.Current.GoToAsync($"//{route}?id={id}");
    }
}
