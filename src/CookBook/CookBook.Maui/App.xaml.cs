using CookBook.Mobile.Shells;

namespace CookBook.Maui
{
    public partial class App : Application
    {
        private readonly IServiceProvider serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
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
    }
}