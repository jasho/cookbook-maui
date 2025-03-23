using CookBook.Common.Models;
using CookBook.Maui.Facades.Interfaces;
using CookBook.Mobile.Api;
using System.Linq;

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

    public async Task<TDetailModel?> GetByIdAsync(Guid id)
    {
        if(connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                var itemOnline = await GetByIdOnlineAsync(id);
                if(itemOnline is not null)
                {
                    await CreateOrUpdateLocalAsync(itemOnline);

                    return itemOnline;
                }
                else
                {
                    return await GetByIdLocalAsync(id);
                }
            }
            catch (ApiException)
            {
            }
        }
        
        return await GetByIdLocalAsync(id);
    }

    protected abstract Task<ICollection<TListModel>> GetAllItemsOnlineAsync();
    protected abstract Task<ICollection<TListModel>> GetAllItemsLocalAsync();

    protected abstract Task<TDetailModel?> GetByIdOnlineAsync(Guid id);
    protected abstract Task<TDetailModel?> GetByIdLocalAsync(Guid id);

    protected abstract Task CreateOrUpdateOnlineAsync(TDetailModel item);
    protected abstract Task CreateOrUpdateLocalAsync(TDetailModel item);


    public class Dependencies(IConnectivity connectivity)
    {
        public IConnectivity Connectivity { get; } = connectivity;
    }
}
