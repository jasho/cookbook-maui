using Android.App;
using Android.OS;

namespace CookBook.Mobile.Platforms.Android;

[Activity(Name="SomeLongName.ShareActivity")]
public class ShareActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (Intent?.Action == "android.intent.action.SEND"
            && Intent.Type == "text/plain")
        {
            HandleShareUrl();
        }
    }

    private void HandleShareUrl()
    {
        var url = Intent?.GetStringExtra("android.intent.extra.TEXT");
        if (url?.StartsWith("https://") is true)
        {
            var itemId = url.Split('/').Last();

            if (url.Contains("ingredients"))
            {
                Shell.Current.GoToAsync($"//ingredients/detail?id={itemId}");
            }
            else if (url.Contains("recipes"))
            {
                Shell.Current.GoToAsync($"//recipes/detail?id={itemId}");
            }
        }
    }
}
