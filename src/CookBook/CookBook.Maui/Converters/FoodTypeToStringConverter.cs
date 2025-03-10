using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;
using CookBook.Maui.Resources.Texts;
using System.Globalization;

namespace CookBook.Maui.Converters;

[AcceptEmptyServiceProvider]
// ReSharper disable once PartialTypeWithSinglePart
public partial class FoodTypeToStringConverter : BaseConverterOneWay<FoodType, string>
{
    public override string ConvertFrom(FoodType value, CultureInfo? culture)
        => FoodTypeTexts.ResourceManager.GetString(value.ToString(), culture)
           ?? FoodTypeTexts.Unknown;

    public override string DefaultConvertReturnValue { get; set; } = FoodTypeTexts.Unknown;
}