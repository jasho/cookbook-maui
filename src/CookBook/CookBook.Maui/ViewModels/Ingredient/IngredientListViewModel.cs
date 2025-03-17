using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Common.Models;
using CookBook.Maui.Services.Interfaces;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Ingredient;

public partial class IngredientListViewModel(
    IRoutingService routingService,
    IIngredientsClient ingredientsClient)
    : ViewModelBase
{
    [ObservableProperty]
    public partial ICollection<IngredientListModel>? Items { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

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
