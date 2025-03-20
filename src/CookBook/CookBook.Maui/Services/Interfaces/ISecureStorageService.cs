namespace CookBook.Maui.Services.Interfaces
{
    public interface ISecureStorageService
    {
        Task<bool> GetIsFirstRunAsync();
        Task SetIsFirstRunAsync(bool isFirstRun);
    }
}