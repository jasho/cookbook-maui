using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;

namespace CookBook.Mobile.Converters
{
    public class FoodTypeToStringConverter : BaseConverterOneWay<FoodType, string>
    {
        public override string? ConvertFrom(FoodType value)
            => value switch
            {
                FoodType.Unknown => "",
                FoodType.MainDish => "Main Dish",
                FoodType.Soup => "Soup",
                FoodType.Dessert => "Dessert",
                _ => ""
            };
    }
}
