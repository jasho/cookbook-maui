namespace CookBook.App.Resources.Styles;

public static class LabelStyleStatic
{
    private static Style labelStyleBase = new(typeof(Label))
    {
        Setters =
        {
            new Setter { Property = Label.TextColorProperty, Value = ThemeStatic.PrimaryLabelTextColor },
        }
    };

    public static Style MediumLabelStyle { get; set; } = new(typeof(Label))
    {
        BasedOn = labelStyleBase,
        Setters =
        {
            new Setter { Property = Label.FontFamilyProperty, Value = Fonts.Fonts.Medium },
        }
    };

    public static Style BoldLabelStyle { get; set; } = new(typeof(Label))
    {
        BasedOn = labelStyleBase,
        Setters =
        {
            new Setter { Property = Label.FontFamilyProperty, Value = Fonts.Fonts.Bold },
        }
    };
}