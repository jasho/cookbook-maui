using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;
using CookBook.Mobile.Resources.Texts;

namespace CookBook.Mobile.Converters
{
    public class FoodTypeToStringConverter : BaseConverterOneWay<FoodType, string>
    {
        public override string? ConvertFrom(FoodType value)
            => FoodTypeTexts.ResourceManager.GetString(value.ToString());
    }
}
