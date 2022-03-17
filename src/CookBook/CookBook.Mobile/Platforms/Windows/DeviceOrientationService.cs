using CookBook.Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Mobile.Services;

public partial class DeviceOrientationService
{
    public partial DeviceOrientation GetOrientation()
        => DeviceOrientation.Landscape;
}
