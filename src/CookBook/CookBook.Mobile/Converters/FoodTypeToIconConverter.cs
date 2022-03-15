using CommunityToolkit.Maui.Converters;
using CookBook.Common.Enums;
using CookBook.Mobile.Resources.Fonts;

namespace CookBook.Mobile.Converters;

public class FoodTypeToIconConverter : BaseConverterOneWay<FoodType, string>
{
    public override string ConvertFrom(FoodType value)
    {
        var icon = FontAwesomeIcons.ConciergeBell;

        switch (value)
        {
            case FoodType.MainDish:
                icon = FontAwesomeIcons.ConciergeBell;
                break;
            case FoodType.Soup:
                icon = FontAwesomeIcons.UtensilSpoon;
                break;
            case FoodType.Dessert:
                icon = FontAwesomeIcons.IceCream;
                break;
        }

        return icon;
    }
}