//using Android.Widget;
//using CookBook.Mobile.Handlers;
//using Microsoft.Maui.Handlers;

//namespace CookBook.Mobile.Platforms.Android;

//public class CustomEntryHandler : ViewHandler<ICustomEntry, EditText>
//{
//    private static PropertyMapper<ICustomEntry, CustomEntryHandler> CustomEntryMapper = new(ViewMapper)
//    {
//        [nameof(ICustomEntry.Text)] = MapText,
//        [nameof(ICustomEntry.TextColor)] = MapTextColor,
//    };

//    public CustomEntryHandler()
//        : base(CustomEntryMapper)
//    {
//    }

//    protected override EditText CreatePlatformView()
//    {
//        return new EditText(Context);
//    }

//    private static void MapText(CustomEntryHandler handler, ICustomEntry entry)
//    {
//        handler.PlatformView.Text = entry.Text + " custom";
//    }

//    private static void MapTextColor(CustomEntryHandler handler, ICustomEntry entry)
//    {
//        entry.TextColor.ToRgba(out var r, out var g, out var b, out var a);
//        //handler.PlatformView.SetTextColor(new Android.Graphics.Color(r, g, b, a));
//    }
//}