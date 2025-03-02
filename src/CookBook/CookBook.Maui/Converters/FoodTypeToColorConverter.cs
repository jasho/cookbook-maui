using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;
using System.Globalization;

namespace CookBook.Maui.Converters;

public class FoodTypeToColorConverter : BaseConverterOneWay<FoodType, Color>
{
    public override Color ConvertFrom(FoodType value, CultureInfo? culture)
    {
        var color = Grey;

        if (Application.Current?.Resources.TryGetValue($"{value}FoodTypeColor", out var resourceValue) is true
            && resourceValue is Color resourceColor)
        {
            color = resourceColor;
        }

        return color;
    }

    public override Color DefaultConvertReturnValue { get; set; } = Grey;
}
