using CookBook.Maui.Enums;
using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.Shells;

namespace CookBook.Maui
{
    public partial class App
    {
        private readonly IServiceProvider serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            this.serviceProvider = serviceProvider;

            var preferencesService = serviceProvider.GetRequiredService<IPreferencesService>();
            var themeSelectorService = serviceProvider.GetRequiredService<IThemeSelectorService>();
            var languageSelectorService = serviceProvider.GetRequiredService<ILanguageSelectorService>();
            ApplyPreferences(preferencesService, themeSelectorService, languageSelectorService);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Shell appShell;
            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                appShell = serviceProvider.GetRequiredService<AppShellPhone>();
            }
            else
            {
                appShell = serviceProvider.GetRequiredService<AppShellDesktop>();
            }

            return new Window(appShell);
        }

        private void ApplyPreferences(
            IPreferencesService preferencesService,
            IThemeSelectorService themeSelectorService,
            ILanguageSelectorService languageSelectorService)
        {
            var theme = preferencesService.GetAppTheme();
            if (theme == Theme.System)
            {
                theme = ResolveSystemTheme();
            }
            themeSelectorService.SelectTheme(theme);

            var appLanguage = preferencesService.GetAppLanguage();
            languageSelectorService.SelectLanguage(appLanguage);
        }

        private Theme ResolveSystemTheme()
            => RequestedTheme switch
            {
                AppTheme.Light => Theme.Light,
                AppTheme.Dark => Theme.Dark,
                _ => Theme.Dark
            };
    }
}