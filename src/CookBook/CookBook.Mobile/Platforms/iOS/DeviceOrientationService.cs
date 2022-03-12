using CookBook.Mobile.Enums;
using UIKit;

namespace CookBook.Mobile.Services;

public partial class DeviceOrientationService
{
    public partial DeviceOrientation GetOrientation()
    {
        var orientation = UIApplication.SharedApplication.StatusBarOrientation;
        var isPortrait = orientation == UIInterfaceOrientation.Portrait
            || orientation == UIInterfaceOrientation.PortraitUpsideDown;
        return isPortrait ? DeviceOrientation.Portrait : DeviceOrientation.Landscape;
    }
}
