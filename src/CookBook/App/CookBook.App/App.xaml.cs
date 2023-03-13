using CookBook.App.Views;
using ZeroIoC;

namespace CookBook.App;

public partial class App {
    private readonly Container container;

    public App(IServiceProvider serviceProvider) {
        container = new Container();
        container.AddDelegate<HttpClient>(p => new HttpClient(p.Resolve<HttpMessageHandler>())
            { BaseAddress = new Uri("https://app-cookbook-maui-api.azurewebsites.net/")}
        );

        TimingHelper.Log("START");

        InitializeComponent();
        TimingHelper.Log("InitializeComponent END");

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("cs-CZ");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("cs-CZ");

        TimingHelper.Log("CurrentUICulture END");

        var view = container.Resolve<AppShellOptimized>();
        TimingHelper.Log("EmptyView END");

        MainPage = view;

        TimingHelper.Log("END");
    }
}