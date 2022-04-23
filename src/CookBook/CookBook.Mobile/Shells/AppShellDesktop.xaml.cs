namespace CookBook.Mobile.Shells;

public partial class AppShellDesktop
{
    public AppShellDesktop()
    {
        InitializeComponent();
    }

    private void Exit_Clicked(object sender, EventArgs e)
    {
        Application.Current?.Quit();
    }
}