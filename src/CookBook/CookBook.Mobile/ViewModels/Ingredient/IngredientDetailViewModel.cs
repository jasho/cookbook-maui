using CookBook.Mobile.Models;

namespace CookBook.Mobile.ViewModels.Ingredient;

[QueryProperty(nameof(Id), "id")]
public class IngredientDetailViewModel : ViewModelBase
{
    public string? Id { private get; set; }

    public IngredientDetailModel? Ingredient { get; set; } = new();

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        if (Guid.TryParse(Id, out var id))
        {
            Ingredient = new IngredientDetailModel(id, "Vejce", "Popis vejce", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Chicken_egg_2009-06-04.jpg/428px-Chicken_egg_2009-06-04.jpg");
        }
    }
}