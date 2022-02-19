using CookBook.Mobile.Models;

namespace CookBook.Mobile.ViewModels;

public class IngredientListViewModel
{
    public ICollection<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>
    {
        new IngredientListModel
        {
            Id = Guid.NewGuid(),
            Name = "Vejce"
        }
    };
}