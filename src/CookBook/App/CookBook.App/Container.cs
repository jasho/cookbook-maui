using CookBook.App.Views;

namespace CookBook.App;

public partial class Container
{
    public Page? GetAppShell()
        => Resolve(typeof(AppShellOptimized)) as Page;
}