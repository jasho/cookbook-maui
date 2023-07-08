using CommunityToolkit.Maui.Converters;
using CookBook.App.Components.Common.Resources.Texts;
using System.Globalization;

namespace CookBook.App.Converters;

public class RecipeIngredientAmountsCountToStringConverter : BaseConverterOneWay<int, string>
{
    public override string ConvertFrom(int value, CultureInfo? culture)
        => value switch
        {
            0 => string.Format(RecipeDetailViewTexts.RecipeIngredientAmountsCount_Label_Text_0, value),
            1 => string.Format(RecipeDetailViewTexts.RecipeIngredientAmountsCount_Label_Text_1, value),
            _ => string.Format(RecipeDetailViewTexts.RecipeIngredientAmountsCount_Label_Text_MoreThan1, value),
        };

    public override string DefaultConvertReturnValue { get; set; } = string.Empty;
}