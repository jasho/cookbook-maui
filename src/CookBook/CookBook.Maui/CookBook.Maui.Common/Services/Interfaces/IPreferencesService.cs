using CookBook.Maui.Enums;
using System.Globalization;

namespace CookBook.Maui.Services.Interfaces
{
    public interface IPreferencesService
    {
        Theme AppTheme { get; set; }
        CultureInfo AppLanguage { get; set; }
    }
}