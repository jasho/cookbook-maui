namespace CookBook.Maui.Extensions;

public static class TaskExtensions
{
    public static async void SafeFireAndForget(this Task task, Action<Exception>? onException = null)
    {
        try
        {
            await task;
        }
        catch(Exception exception)
        {
            onException?.Invoke(exception);
        }
    }
}