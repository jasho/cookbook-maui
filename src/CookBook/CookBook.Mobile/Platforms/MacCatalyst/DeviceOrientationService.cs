using CookBook.Mobile.Enums;

namespace CookBook.Mobile.Services;

public partial class DeviceOrientationService
{
    public partial DeviceOrientation GetOrientation()
        => DeviceOrientation.Landscape;
}
