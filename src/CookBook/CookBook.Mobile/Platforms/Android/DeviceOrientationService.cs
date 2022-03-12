using Android.Content;
using Android.Runtime;
using Android.Views;
using CookBook.Mobile.Enums;

namespace CookBook.Mobile.Services;

public partial class DeviceOrientationService
{
    public partial DeviceOrientation GetOrientation()
    {
        var windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
        var orientation = windowManager.DefaultDisplay.Rotation;
        bool isLandscape = orientation == SurfaceOrientation.Rotation90 || orientation == SurfaceOrientation.Rotation270;
        return isLandscape ? DeviceOrientation.Landscape : DeviceOrientation.Portrait;
    }
}
