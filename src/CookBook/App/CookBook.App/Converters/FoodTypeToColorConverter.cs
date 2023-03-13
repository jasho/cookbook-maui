using CommunityToolkit.Maui.Converters;
using CookBook.App.Resources.Styles;
using CookBook.Common.Enums;
using System.Globalization;

namespace CookBook.App.Converters;

public class FoodTypeToColorConverter : BaseConverterOneWay<FoodType, Color>
{
    public override Color ConvertFrom(FoodType value, CultureInfo? culture)
        => value switch
        {
            FoodType.Unknown => ThemeStatic.UnknownFoodTypeColor,
            FoodType.MainDish => ThemeStatic.MainDishFoodTypeColor,
            FoodType.Soup => ThemeStatic.SoupFoodTypeColor,
            FoodType.Dessert => ThemeStatic.DessertFoodTypeColor,
            _ => DefaultConvertReturnValue
        };

    public override Color DefaultConvertReturnValue { get; set; } = Grey;
}
