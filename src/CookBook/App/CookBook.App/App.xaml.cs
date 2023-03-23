using CookBook.App.Shells;

namespace CookBook.App;

public partial class App
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            MainPage = serviceProvider.GetRequiredService<AppShellPhone>();
        }
        else
        {
            MainPage = serviceProvider.GetRequiredService<AppShellDesktop>();
        }
    }
}