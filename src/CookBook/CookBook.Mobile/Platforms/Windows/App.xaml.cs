using CookBook.Mobile.Services;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CookBook.Mobile.Platforms.Windows {
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        private IGlobalExceptionService globalExceptionService;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;
        }

        private void OnAppUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            globalExceptionService.HandleException(e.Exception);
        }

        protected override MauiApp CreateMauiApp()
        {
            var mauiApp = MauiProgram.CreateMauiApp();
            globalExceptionService = mauiApp.Services.GetRequiredService<IGlobalExceptionService>();

            return mauiApp;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            Platform.OnLaunched(args);
        }
    }
}
