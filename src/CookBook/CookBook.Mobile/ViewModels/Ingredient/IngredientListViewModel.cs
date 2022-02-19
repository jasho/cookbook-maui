using CookBook.Mobile.Factories;
using CookBook.Mobile.Models;
using System.Windows.Input;

namespace CookBook.Mobile.ViewModels;

public class IngredientListViewModel
{
    public ICollection<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>
    {
        new IngredientListModel(
            Guid.NewGuid(),
            "Vejce",
            "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Chicken_egg_2009-06-04.jpg/428px-Chicken_egg_2009-06-04.jpg")
    };

    public ICommand GoToDetailCommand { get; set; }

    public IngredientListViewModel(ICommandFactory commandFactory)
    {
        GoToDetailCommand = commandFactory.CreateCommand<Guid>(GoToDetailAsync);
    }

    private async Task GoToDetailAsync(Guid id)
    {
        //await Shell.Current.GoToAsync($"//ingredients/detail?id={id}");
        await Shell.Current.GoToAsync($"//ingredients/detail?id={id}");
    }
}
