using CommunityToolkit.Maui.Converters;
using CookBook.Mobile.Resources.Texts;
using System.Globalization;
using CookBook.Entities.Enums;

namespace CookBook.Mobile.Converters;

public class UnitToStringConverter : BaseConverterOneWay<Unit, string>
{
	public override string ConvertFrom(Unit value, CultureInfo? culture)
		=> UnitTexts.ResourceManager.GetString(value.ToString(), culture)
		   ?? UnitTexts.Unknown;

	public override string DefaultConvertReturnValue { get; set; } = UnitTexts.Unknown;
}
