using Android.App;
using Android.Runtime;
using Android.Util;

namespace CookBook.App.Platforms.Android;

[Application(Theme = "@style/Maui.SplashTheme")]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
        TimingHelper.Start(t => Log.Info("~LOG~", t));
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}