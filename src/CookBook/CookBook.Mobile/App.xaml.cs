using CookBook.Mobile.Shells;

namespace CookBook.Mobile;

public partial class App
{
    public App()
    {
        InitializeComponent();

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("cs-CZ");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("cs-CZ");

        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            MainPage = new AppShellPhone();
        }
        else
        {
            MainPage = new AppShellDesktop();
        }
    }
}