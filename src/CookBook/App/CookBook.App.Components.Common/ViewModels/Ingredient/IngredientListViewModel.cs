using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.App.Components.Common.Api;
using CookBook.App.Components.Common.Services;
using CookBook.Common.Models;

namespace CookBook.App.Components.Common.ViewModels;

public partial class IngredientListViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;
    private readonly IIngredientsClient ingredientsClient;

    [ObservableProperty]
    private ICollection<IngredientListModel>? items;

    public IngredientListViewModel(
        IRoutingService routingService,
        IIngredientsClient ingredientsClient)
    {
        this.routingService = routingService;
        this.ingredientsClient = ingredientsClient;
    }

    public override async Task OnAppearingAsync()
    {
        Items = await ingredientsClient.GetIngredientsAllAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        //var route = routingService.GetRouteByViewModel<IngredientDetailViewModel>();
        await Shell.Current.GoToAsync("//ingredients/detail", new Dictionary<string, object>
        {
            [nameof(IngredientDetailViewModel.Id)] = id
        });
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        var route = routingService.GetRouteByViewModel<IngredientEditViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    [RelayCommand]
    private async Task GoToEditAsync(Guid id)
    {
        var route = routingService.GetRouteByViewModel<IngredientEditViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
    }
}
