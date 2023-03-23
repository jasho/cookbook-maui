namespace CookBook.App;

public partial class AppOptimized
{
	public AppOptimized()
	{
        var container = new ContainerOptimized();
        //container.AddDelegate<HttpClient>(p => new HttpClient(p.Resolve<HttpMessageHandler>())
        //{ BaseAddress = new Uri("https://app-cookbook-maui-api.azurewebsites.net/") }
        //);

        InitializeComponent();

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

        MainPage = container.GetAppShell();
	}
}