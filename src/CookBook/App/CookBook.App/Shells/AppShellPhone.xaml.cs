namespace CookBook.App.Shells;

public partial class AppShellPhone
{
	public AppShellPhone()
	{
        TimingHelper.Log(message: "START");
		InitializeComponent();
        TimingHelper.Log(message: "END");
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        TimingHelper.Log(message: "END");
    }
}