using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;

namespace CookBook.Mobile.Converters
{
    public class FoodTypeToColorConverter : BaseConverterOneWay<FoodType, Color>
    {
        public override Color? ConvertFrom(FoodType value)
            => value switch
            {
                FoodType.Unknown => Colors.Grey,
                FoodType.MainDish => Colors.Blue,
                FoodType.Soup => Colors.Yellow,
                FoodType.Dessert => Colors.Red,
                _ => throw new NotImplementedException(),
            };
    }
}
