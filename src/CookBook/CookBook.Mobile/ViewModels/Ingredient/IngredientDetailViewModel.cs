using CookBook.Mobile.Models;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), "id")]
public class IngredientDetailViewModel
{
    public string Id { set => LoadDataAsync(Guid.Parse(value)); }

    public IngredientDetailModel? Ingredient { get; set; }

    public async Task LoadDataAsync(Guid id)
    {
        Ingredient = new IngredientDetailModel(id, "Vejce", "Popis vejce", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Chicken_egg_2009-06-04.jpg/428px-Chicken_egg_2009-06-04.jpg");
    }
}