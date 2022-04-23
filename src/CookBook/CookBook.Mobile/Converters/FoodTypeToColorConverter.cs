using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;
using System.Globalization;

namespace CookBook.Mobile.Converters;

public class FoodTypeToColorConverter : BaseConverterOneWay<FoodType, Color>
{
    public override Color ConvertFrom(FoodType value, CultureInfo? culture)
    {
        var color = Grey;

        if (Application.Current.Resources.TryGetValue($"{value}FoodTypeColor", out var resourceColor))
        {
            color = resourceColor as Color;
        }

        return color;
    }
}
