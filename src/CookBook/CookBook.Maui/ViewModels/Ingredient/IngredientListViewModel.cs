using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Interfaces;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Ingredient;

[INotifyPropertyChanged]
public partial class IngredientListViewModel : IViewModel
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

    public async Task OnAppearingAsync()
    {
        Items = await ingredientsClient.GetIngredientsAllAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        var route = routingService.GetRouteByViewModel<IngredientDetailViewModel>();
        await Shell.Current.GoToAsync(route, new Dictionary<string, object>
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
