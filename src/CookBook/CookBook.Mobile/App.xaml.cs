using CookBook.Mobile.Shells;

namespace CookBook.Mobile;

public partial class App
{
    public App()
    {
        InitializeComponent();

        if (Device.Idiom == TargetIdiom.Phone)
        {
            MainPage = new AppShellPhone();
        }
        else
        {
            MainPage = new AppShellDesktop();
        }
    }
}