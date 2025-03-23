using CookBook.Common.Models;
using CookBook.Maui.Facades.Interfaces;
using CookBook.Mobile.Api;

namespace CookBook.Maui.Facades;

public abstract class FacadeBase<TListModel, TDetailModel>
    : IFacade<TListModel, TDetailModel>
    where TListModel : ModelBase
    where TDetailModel : ModelBase
{
    private readonly IConnectivity connectivity;

    public FacadeBase(Dependencies dependencies)
    {
        connectivity = dependencies.Connectivity;
    }

    public async Task<IList<TListModel>> GetAllItemsAsync()
    {
        ICollection<TListModel> itemsOnline = [];
        
        if(connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                itemsOnline = await GetAllItemsOnlineAsync();
            }
            catch (ApiException)
            {
            }
        }

        var itemsLocal = await GetAllItemsLocalAsync();

        return itemsOnline
            .Union(itemsLocal)
            .DistinctBy(item => item.Id)
            .ToList();
    }

    protected abstract Task<ICollection<TListModel>> GetAllItemsOnlineAsync();
    protected abstract Task<ICollection<TListModel>> GetAllItemsLocalAsync();

    public class Dependencies(IConnectivity connectivity)
    {
        public IConnectivity Connectivity { get; } = connectivity;
    }
}
