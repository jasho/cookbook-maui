using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.App.Components.Common.Api;
using CookBook.App.Components.Common.Services;
using CookBook.Common.Models;

namespace CookBook.App.Components.Common.ViewModels;

public partial class IngredientListViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;

    [ObservableProperty]
    private ICollection<IngredientListModel>? items;

    public IngredientListViewModel(
        IIngredientsClient ingredientsClient,
        INavigationService navigationService)
        : base(navigationService)
    {
        this.ingredientsClient = ingredientsClient;
    }

    public override async Task OnAppearingAsync()
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
