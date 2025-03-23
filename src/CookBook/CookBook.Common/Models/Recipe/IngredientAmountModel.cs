using CookBook.Common.Enums;
using NJsonSchema.Annotations;

namespace CookBook.Common.Models;

public class IngredientAmountModel : ModelBase
{
    public double Amount { get; set; }
    public Unit Unit { get; set; }
    public IngredientListModel? Ingredient { get; set; }
}