using CookBook.Maui.Enums;
using System.Globalization;

namespace CookBook.Maui.Services.Interfaces
{
    public interface IPreferencesService
    {
        Theme GetAppTheme();
        void SetAppTheme(Theme theme);
        CultureInfo GetAppLanguage();
        void SetAppLanguage(CultureInfo cultureInfo);
    }
}