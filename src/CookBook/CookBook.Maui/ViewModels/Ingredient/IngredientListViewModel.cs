using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Models;
using CookBook.Maui.Facades;
using CookBook.Maui.Messages;
using CookBook.Maui.Services;

namespace CookBook.Maui.ViewModels.Ingredient;

public partial class IngredientListViewModel
    : ViewModelBase, IRecipient<IngredientCreatedOrUpdatedMessage>
{
    private readonly IIngredientsFacade ingredientsFacade;

    public IngredientListViewModel(
        IIngredientsFacade ingredientsFacade)
    {
        this.ingredientsFacade = ingredientsFacade;

        IsActive = true;
    }

    [ObservableProperty]
    public partial ICollection<IngredientListModel>? Items { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Items = await ingredientsFacade.GetAllItemsAsync();
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

    public void Receive(IngredientCreatedOrUpdatedMessage message)
    {
        ForceDataRefresh = true;
    }
}
