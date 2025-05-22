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

        protected override void OnStart()
        {
            base.OnStart();

            var databaseService = serviceProvider.GetRequiredService<IDatabaseService>();
            databaseService.InitializeDatabase();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(serviceProvider.GetRequiredService<AppShell>());
        }

        private void ApplyPreferences(
            IPreferencesService preferencesService,
            IThemeSelectorService themeSelectorService,
            ILanguageSelectorService languageSelectorService)
        {
            var theme = preferencesService.AppTheme;
            if (theme == Theme.System)
            {
                theme = ResolveSystemTheme();
            }
            themeSelectorService.SelectTheme(theme);

            languageSelectorService.SelectLanguage(preferencesService.AppLanguage);
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