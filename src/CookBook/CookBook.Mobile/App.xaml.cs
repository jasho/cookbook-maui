using CookBook.Mobile.Shells;

namespace CookBook.Mobile;

public partial class App
{
    public App()
    {
        InitializeComponent();

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