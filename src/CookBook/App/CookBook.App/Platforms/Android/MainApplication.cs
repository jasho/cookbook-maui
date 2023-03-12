using Android.App;
using Android.Runtime;

namespace CookBook.App.Platforms.Android;

[Application(Theme = "@style/Maui.SplashTheme")]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}