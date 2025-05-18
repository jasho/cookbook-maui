using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.Maui.Enums;
using CookBook.Maui.Services.Interfaces;
using System.Globalization;

namespace CookBook.Maui.ViewModels;

public partial class SettingsViewModel(
    IPreferencesService preferencesService,
    IThemeSelectorService themeSelectorService,
    ILanguageSelectorService languageSelectorService)
    : ViewModelBase
{
    [ObservableProperty]
    public partial Theme SelectedTheme { get; set; }

    [ObservableProperty]
    public partial CultureInfo? SelectedLanguage { get; set; }

    public IEnumerable<Theme> AvailableThemes { get; } = (Theme[])typeof(Theme).GetEnumValues();

    public IEnumerable<CultureInfo> AvailableLanguages { get; } =
    [
        CultureInfo.GetCultureInfo("cs"),
        CultureInfo.GetCultureInfo("en")
    ];

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        SelectedTheme = preferencesService.AppTheme;
        SelectedLanguage = preferencesService.AppLanguage;
    }

    [RelayCommand]
    private void Save()
    {
        themeSelectorService.SelectTheme(SelectedTheme);
        preferencesService.AppTheme = SelectedTheme;

        if (SelectedLanguage is not null)
        {
            languageSelectorService.SelectLanguage(SelectedLanguage);
            preferencesService.AppLanguage = SelectedLanguage;
        }

        Shell.Current.SendBackButtonPressed();
    }
}