using System.Globalization;

namespace CookBook.Maui.Services.Interfaces
{
    public interface ILanguageSelectorService
    {
        void SelectLanguage(CultureInfo cultureInfo);
    }
}