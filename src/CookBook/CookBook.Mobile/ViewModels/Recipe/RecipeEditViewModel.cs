using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Mobile.Api;

namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Recipe), "recipe")]
public partial class RecipeEditViewModel : ViewModelBase
{
    private readonly IRecipesClient recipesClient;
    
    public RecipeDetailModel Recipe { get; set; }

    public List<FoodType> FoodTypes { get; set; }

    public RecipeEditViewModel(IRecipesClient recipesClient)
    {
        this.recipesClient = recipesClient;

        FoodTypes = new List<FoodType>((FoodType[])Enum.GetValues(typeof(FoodType)));
    }
}