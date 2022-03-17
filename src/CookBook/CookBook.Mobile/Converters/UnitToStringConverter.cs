using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;
using CookBook.Mobile.Resources.Texts;

namespace CookBook.Mobile.Converters;

public class UnitToStringConverter : BaseConverterOneWay<Unit, string>
{
    public override string? ConvertFrom(Unit value)
        => UnitTexts.ResourceManager.GetString(value.ToString());
}
