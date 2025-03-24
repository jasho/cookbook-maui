using CookBook.Common.Enums;
using NJsonSchema.Annotations;
using System.Collections.ObjectModel;

namespace CookBook.Common.Models;

[JsonSchemaFlatten]
public class RecipeDetailModel : ModelBase
{
    public RecipeDetailModel()
    {
    }

    public RecipeDetailModel(RecipeDetailModel recipe)
    {
        Id = recipe.Id;
        Name = recipe.Name;
        Description = recipe.Description;
        Duration = recipe.Duration;
        FoodType = recipe.FoodType;
        IngredientAmounts = recipe.IngredientAmounts;
        ImageUrl = recipe.ImageUrl;
    }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public TimeSpan Duration { get; set; }
    public FoodType FoodType { get; set; }
    public IList<IngredientAmountModel>? IngredientAmounts { get; set; } = new ObservableCollection<IngredientAmountModel>();
    public string? ImageUrl { get; set; }
}