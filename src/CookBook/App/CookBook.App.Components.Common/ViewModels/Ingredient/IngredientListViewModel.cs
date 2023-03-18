using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.App.Components.Common.Api;
using CookBook.App.Components.Common.Services;
using CookBook.Common.Models;

namespace CookBook.App.Components.Common.ViewModels;

public partial class IngredientListViewModel : ViewModelBase
{
    private readonly INavigationService navigationService;
    private readonly IIngredientsClient ingredientsClient;

    [ObservableProperty]
    private ICollection<IngredientListModel>? items;

    public IngredientListViewModel(
        INavigationService navigationService,
        IIngredientsClient ingredientsClient)
    {
        this.navigationService = navigationService;
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

    [RelayCommand]
    private async Task GoToEditAsync(Guid id)
    {
        var ingredientListModel = Items?.SingleOrDefault(item => item.Id == id);

        if (ingredientListModel is not null)
        {
            await navigationService.GoToAsync<IngredientEditViewModel>();
        }
    }
}
