using Akavache;
using Foundation;
using UIKit;

namespace CookBook.Mobile.Platforms.iOS;

[Register(nameof(AppDelegate))]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override void WillEnterForeground(UIApplication application)
    {
        BlobCache.EnsureInitialized();
        base.WillEnterForeground(application);
    }

    public override void DidEnterBackground(UIApplication application)
    {
        BlobCache.Shutdown().Wait(TimeSpan.FromMilliseconds(500));
        base.DidEnterBackground(application);
    }
}