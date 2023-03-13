namespace CookBook.App.Resources.Styles
{
    public static class ButtonStyleStatic
    {
        public static Style PrimaryButtonStyle { get; set; } = new(typeof(Button))
        {
            Setters =
            {
                new Setter{ Property = Button.TextColorProperty, Value = ThemeStatic.PrimaryButtonTextColor },
                new Setter{ Property = VisualElement.BackgroundColorProperty, Value = ThemeStatic.PrimaryColor },
                new Setter{ Property = Button.FontFamilyProperty, Value = Fonts.Fonts.Regular },
            }
        };
    }
}