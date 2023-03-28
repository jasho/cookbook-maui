using CookBook.Mobile.Services;
using CookBook.Mobile.Shells;

namespace CookBook.Mobile;

public partial class App
{
    private readonly IServiceProvider serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        InitializeComponent();

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("cs-CZ");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("cs-CZ");

        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            MainPage = serviceProvider.GetRequiredService<AppShellPhone>();
        }
        else
        {
            MainPage = serviceProvider.GetRequiredService<AppShellDesktop>();
        }
    }

    protected override void OnStart()
    {
        base.OnStart();

        var globalExceptionServiceInitializer = serviceProvider.GetRequiredService<IGlobalExceptionServiceInitializer>();
        globalExceptionServiceInitializer.Initialize();
    }
}