using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CookBook.Common.Models;
using CookBook.Maui.Messages;
using CookBook.Mobile.Api;

namespace CookBook.Maui.ViewModels.Ingredient;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class IngredientDetailViewModel : ViewModelBase, IRecipient<IngredientUpdatedMessage>
{
    private readonly IIngredientsClient ingredientsClient;
    private readonly IShare share;

    /// <inheritdoc/>
    public IngredientDetailViewModel(
        IIngredientsClient ingredientsClient,
        IShare share)
    {
        this.ingredientsClient = ingredientsClient;
        this.share = share;

        IsActive = true;
    }

    public Guid Id { get; init; } = Guid.Empty;

    [ObservableProperty]
    public partial IngredientDetailModel? Ingredient { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (Id != Guid.Empty)
        {
            Ingredient = await ingredientsClient.GetIngredientByIdAsync(Id);
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Ingredient?.Id is not null)
        {
            await ingredientsClient.DeleteIngredientAsync(Ingredient.Id.Value);
        }

        Shell.Current.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        Dictionary<string, object> navigationParameters = [];

        if (Ingredient?.Id is not null)
        {
            navigationParameters.Add(nameof(IngredientEditViewModel.Id), Ingredient.Id.Value);
        }

        await Shell.Current.GoToAsync("/edit", navigationParameters);
    }

    [RelayCommand]
    private async Task ShareAsync()
    {
        if (Ingredient?.Id is not null)
        {
            await share.RequestAsync(new ShareTextRequest
            {
                Title = Ingredient.Name,
                Text = $"""
                        {Ingredient.Name}

                        {Ingredient.Description}
                        """,
                Subject = $"CookBook - {Ingredient.Name}",
                Uri = $"https://app-cookbook-maui-api.azurewebsites.net/api/ingredients/{Ingredient.Id.Value}"
            });
        }
    }

    public void Receive(IngredientUpdatedMessage message)
    {
        if (message.IngredientId == Id)
        {
            ForceDataRefresh = true;
        }
    }
}