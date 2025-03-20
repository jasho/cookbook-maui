using CookBook.Maui.Enums;
using CookBook.Maui.Services.Interfaces;
using System.Globalization;

namespace CookBook.Maui.Services;

public class PreferencesService(IPreferences preferences)
    : IPreferencesService
{
    public const string ThemeKey = "Theme";
    public const string LanguageKey = "Language";

    public Theme AppTheme
    {
        get => (Theme)preferences.Get(ThemeKey, (int)Theme.System);
        set => preferences.Set(ThemeKey, (int)value);
    }

    public CultureInfo AppLanguage
    {
        get
        {
            var language = preferences.Get(LanguageKey, Thread.CurrentThread.CurrentCulture.IetfLanguageTag.Split('-')[0]);
            return new CultureInfo(language);
        }

        set => preferences.Set(LanguageKey, value.TwoLetterISOLanguageName);
    }
}