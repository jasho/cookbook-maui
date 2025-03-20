using CookBook.Maui.Enums;
using CookBook.Maui.Services.Interfaces;
using System.Globalization;

namespace CookBook.Maui.Services;

public class PreferencesService(IPreferences preferences)
    : IPreferencesService
{
    public const string ThemeKey = "Theme";
    public const string LanguageKey = "Language";

    public Theme GetAppTheme()
    {
        return (Theme)preferences.Get(ThemeKey, (int)Theme.System);
    }

    public void SetAppTheme(Theme theme)
    {
        preferences.Set(ThemeKey, (int)theme);
    }

    public CultureInfo GetAppLanguage()
    {
        var language = preferences.Get(LanguageKey, Thread.CurrentThread.CurrentCulture.IetfLanguageTag.Split('-')[0]);
        return new CultureInfo(language);
    }

    public void SetAppLanguage(CultureInfo cultureInfo)
    {
        preferences.Set(LanguageKey, cultureInfo.TwoLetterISOLanguageName);
    }
}