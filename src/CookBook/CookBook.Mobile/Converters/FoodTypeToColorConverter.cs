﻿using CommunityToolkit.Maui.Converters;
using System.Globalization;
using CookBook.Entities.Enums;

namespace CookBook.Mobile.Converters;

public class FoodTypeToColorConverter : BaseConverterOneWay<FoodType, Color>
{
	public override Color ConvertFrom(FoodType value, CultureInfo? culture)
	{
		var color = Grey;

		if (Application.Current?.Resources.TryGetValue($"{value}FoodTypeColor", out var resourceValue) is true
			&& resourceValue is Color resourceColor)
		{
			color = resourceColor;
		}

		return color;
	}

	public override Color DefaultConvertReturnValue { get; set; } = Grey;
}
