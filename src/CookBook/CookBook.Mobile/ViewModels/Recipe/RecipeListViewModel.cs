using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Factories;
using CookBook.Mobile.Services;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels.Recipe;

public class RecipeListViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;
    private readonly IRecipesClient recipesClient;

    public ICollection<RecipeListModel>? Items { get; set; }

    public ICommand GoToDetailCommand { get; set; }
    public ICommand GoToCreateCommand { get; set; }

    public RecipeListViewModel(
        IRoutingService routingService,
        ICommandFactory commandFactory,
        IRecipesClient recipesClient)
    {
        this.routingService = routingService;
        this.recipesClient = recipesClient;

        GoToDetailCommand = commandFactory.CreateCommand<Guid>(GoToDetailAsync);
        GoToCreateCommand = commandFactory.CreateCommand<Guid>(GoToCreateAsync);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        Items = await recipesClient.GetRecipesAllAsync();
    }

    private async Task GoToDetailAsync(Guid id)
    {
    }

    private async Task GoToCreateAsync(Guid id)
    {
    }
}