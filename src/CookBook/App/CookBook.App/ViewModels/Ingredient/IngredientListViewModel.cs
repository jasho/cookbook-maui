using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.App.Api;
using CookBook.Common.Models;

namespace CookBook.App.ViewModels;

public partial class IngredientListViewModel : ViewModelBase, IViewModel
{
    private readonly IRoutingService routingService;
    private readonly IIngredientsClient ingredientsClient;

    [ObservableProperty]
    private ICollection<IngredientListModel>? items;

    public IngredientListViewModel(
        IRoutingService routingService,
        IIngredientsClient ingredientsClient,
        INavigationService navigationService)
        : base(navigationService)
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
        await navigationService.GoToAsync<IngredientDetailViewModel>(new Dictionary<string, object>
        {
            [nameof(IngredientDetailViewModel.Id)] = id
        });
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync<IngredientEditViewModel>();
    }
}
