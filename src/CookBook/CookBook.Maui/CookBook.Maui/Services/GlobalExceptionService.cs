using CookBook.Maui.Services.Interfaces;

namespace CookBook.Maui.Services
{
    public class GlobalExceptionService : IGlobalExceptionService
    {
        public void HandleException(Exception exception, string? source = null)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Application.Current?.Windows[0].Page?.DisplayAlert("Something went wrong", exception.Message, "OK");
            });
        }
    }
}