using CookBook.Common.Models;

namespace CookBook.Maui.Facades.Interfaces;

public interface IFacade<TListModel, TDetailModel>
    where TListModel: ModelBase
    where TDetailModel : ModelBase
{
    Task<IList<TListModel>> GetAllItemsAsync();
    Task<TDetailModel?> GetByIdAsync(Guid id);
    Task<Guid> CreateOrUpdateAsync(TDetailModel detailModel);
    Task DeleteAsync(Guid id);
}
