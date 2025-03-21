﻿using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;
using CookBook.Maui.Resources.Texts;
using System.Globalization;

namespace CookBook.Maui.Converters;

[AcceptEmptyServiceProvider]
// ReSharper disable once PartialTypeWithSinglePart
public partial class UnitToStringConverter : BaseConverterOneWay<Unit, string>
{
    public override string ConvertFrom(Unit value, CultureInfo? culture)
        => UnitTexts.ResourceManager.GetString(value.ToString(), culture) 
           ?? UnitTexts.Unknown;

    public override string DefaultConvertReturnValue { get; set; } = UnitTexts.Unknown;
}
