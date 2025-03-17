using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Models;
using CookBook.Maui.Messages;
using CookBook.Maui.Services;
using CookBook.Maui.Services.Interfaces;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Ingredient;

public partial class IngredientListViewModel
    : ViewModelBase, IRecipient<IngredientCreatedMessage>, IRecipient<IngredientUpdatedMessage>
{
    private readonly IRoutingService routingService;
    private readonly IIngredientsClient ingredientsClient;

    public IngredientListViewModel(IRoutingService routingService,
        IIngredientsClient ingredientsClient)
    {
        this.routingService = routingService;
        this.ingredientsClient = ingredientsClient;

        IsActive = true;
    }

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
        await Shell.Current.GoToAsync(RoutingService.IngredientDetailRouteRelative, new Dictionary<string, object>
        {
            [nameof(IngredientDetailViewModel.Id)] = id
        });
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await Shell.Current.GoToAsync(RoutingService.IngredientEditRouteRelative);
    }

    [RelayCommand]
    private async Task GoToEditAsync(Guid id)
    {
        await Shell.Current.GoToAsync(RoutingService.IngredientEditRouteRelative,
            new Dictionary<string, object>
            {
                [nameof(IngredientEditViewModel.Id)] = id
            });
    }

    public void Receive(IngredientCreatedMessage message)
    {
        ForceDataRefresh = true;
    }

    public void Receive(IngredientUpdatedMessage message)
    {
        ForceDataRefresh = true;
    }
}
