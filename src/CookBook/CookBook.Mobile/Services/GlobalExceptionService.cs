using Microsoft.Maui.Controls.Platform;

namespace CookBook.Mobile.Services
{
    public class GlobalExceptionService : IGlobalExceptionService
    {
        public void HandleException(Exception exception, string? source = null)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Application.Current?.MainPage?.DisplayAlert("Something went wrong", exception.Message, "OK");
            });
        }
    }
}