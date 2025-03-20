using CookBook.Maui.Services.Interfaces;

namespace CookBook.Maui.Services;

public class SecureStorageService(ISecureStorage secureStorage) : ISecureStorageService
{
    public const string IsFirstRunKey = "IsFirstRun";

    public async Task<bool> GetIsFirstRunAsync()
    {
        var isFirstRunValueStorage = await secureStorage.GetAsync(IsFirstRunKey);
        if (isFirstRunValueStorage is null)
        {
            return true;
        }
        else
        {
            if (bool.TryParse(isFirstRunValueStorage, out var isFirstRun))
            {
                return isFirstRun;
            }
        }

        return true;
    }

    public async Task SetIsFirstRunAsync(bool isFirstRun)
    {
        await secureStorage.SetAsync(IsFirstRunKey, isFirstRun.ToString());
    }
}