using CookBook.Common.Enums;
using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

[JsonSchemaFlatten]
public class RecipeDetailIngredientModel : ModelBase
{
    public Guid? Id { get; set; }
    public double Amount { get; set; }
    public Unit Unit { get; set; }
    public IngredientListModel? Ingredient { get; set; }
}