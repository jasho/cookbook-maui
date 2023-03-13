namespace CookBook.App.Resources.Styles;

public class ContentPageStyleStatic
{
    public static Style ContentPageStyle { get; set; } = new(typeof(ContentPage))
    {
        Setters =
        {
            new Setter{ Property = Page.PaddingProperty, Value = new OnIdiom<Thickness>
            {
                Phone = new Thickness(30, 30, 30, 0),
                Desktop = new Thickness(30),
            }},
            new Setter { Property = VisualElement.BackgroundColorProperty, Value = ThemeStatic.BackgroundColor }
        }
    };
}