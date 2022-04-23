using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;
using CookBook.Mobile.Resources.Texts;
using System.Globalization;

namespace CookBook.Mobile.Converters
{
    public class FoodTypeToStringConverter : BaseConverterOneWay<FoodType, string>
    {
        public override string ConvertFrom(FoodType value, CultureInfo? culture)
            => FoodTypeTexts.ResourceManager.GetString(value.ToString(), culture)
                ?? FoodTypeTexts.Unknown;
    }
}
