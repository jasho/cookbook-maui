using CookBook.Maui.Services.Interfaces;
using System.Globalization;

namespace CookBook.Maui.Services;

public class LanguageSelectorService : ILanguageSelectorService
{
    public void SelectLanguage(CultureInfo cultureInfo)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        });
    }
}