using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Mobile.Api;
using CookBook.Mobile.Commands;
using CookBook.Mobile.Services;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels;

[INotifyPropertyChanged]
public partial class RecipeListViewModel : IViewModel
{
    private readonly IRoutingService routingService;
    private readonly IRecipesClient recipesClient;

    [ObservableProperty]
    private ICollection<RecipeListModel>? items;

    public ICommand GoToDetailCommand { get; set; }

    public RecipeListViewModel(
        IRoutingService routingService,
        IRecipesClient recipesClient,
        ICommandFactory commandFactory)
    {
        this.routingService = routingService;
        this.recipesClient = recipesClient;

        GoToDetailCommand = commandFactory.Create<Guid>(GoToDetailAsync);
    }

    public async Task OnAppearingAsync()
    {
        Items = await recipesClient.GetRecipesAllAsync();
    }

    //[RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task GoToDetailAsync(Guid id)
    {
        var route = routingService.GetRouteByViewModel<RecipeDetailViewModel>();

        await Shell.Current.GoToAsync($"{route}", new Dictionary<string, object> { ["id"] = id });
    }

    [RelayCommand]
    private Task GoToCreateAsync()
        => Task.CompletedTask;
}