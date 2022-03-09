using CookBook.Common.Models;
using CookBook.Mobile.Api;

namespace CookBook.Mobile.ViewModels.Ingredient;

[QueryProperty(nameof(Id), "id")]
public class IngredientDetailViewModel : ViewModelBase
{
    private readonly IIngredientsClient ingredientsClient;

    public string? Id { private get; set; }

    public IngredientDetailModel? Ingredient { get; set; }

    public IngredientDetailViewModel(IIngredientsClient ingredientsClient)
    {
        this.ingredientsClient = ingredientsClient;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        
        if (Guid.TryParse(Id, out var id))
        {
            Ingredient = await ingredientsClient.GetIngredientByIdAsync(id);
        }
    }
}