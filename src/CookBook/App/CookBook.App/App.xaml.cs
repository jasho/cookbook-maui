using ZeroIoC;

namespace CookBook.App;

public partial class App
{
    public App()
    {
        var container = new ContainerOptimized();
        //container.AddDelegate<HttpClient>(p => new HttpClient(p.Resolve<HttpMessageHandler>())
        //{ BaseAddress = new Uri("https://app-cookbook-maui-api.azurewebsites.net/") }
        //);

        InitializeComponent();

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("cs-CZ");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("cs-CZ");

        MainPage = container.GetAppShell();
    }
}