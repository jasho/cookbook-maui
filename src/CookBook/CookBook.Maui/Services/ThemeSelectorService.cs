using CookBook.Maui.Enums;
using CookBook.Maui.Resources.Styles;
using CookBook.Maui.Services.Interfaces;

namespace CookBook.Maui.Services;

public class ThemeSelectorService : IThemeSelectorService
{
    public void SelectTheme(Theme theme)
    {
        if (Application.Current is null)
        {
            return;
        }

        if (theme == Theme.Light)
        {
            var darkTheme = Application.Current.Resources.MergedDictionaries.OfType<DarkTheme>().FirstOrDefault();
            if (darkTheme is not null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(darkTheme);
            }

            Application.Current.Resources.MergedDictionaries.Add(new LightTheme());
        }
        else
        {
            var lightTheme = Application.Current.Resources.MergedDictionaries.OfType<LightTheme>().FirstOrDefault();
            if (lightTheme is not null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(lightTheme);
            }

            Application.Current.Resources.MergedDictionaries.Add(new DarkTheme());
        }
    }
}