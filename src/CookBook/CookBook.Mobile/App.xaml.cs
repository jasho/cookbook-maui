using Microsoft.Maui.Platform;

namespace CookBook.Mobile;

public partial class App
{
    public App()
    {
        InitializeComponent();

        Microsoft.Maui.Handlers.EntryHandler.EntryMapper.AppendToMapping(nameof(IView.Background), (handler, view) =>
        {
#if ANDROID
            handler.NativeView.SetBackgroundColor(Colors.Red.ToNative());
#elif IOS
            handler.NativeView.BackgroundColor = Colors.Blue.ToNative();
#elif WINDOWS
            handler.NativeView.Background = Colors.Green.ToNative();
#endif
        });

        MainPage = new AppShell();
    }
}