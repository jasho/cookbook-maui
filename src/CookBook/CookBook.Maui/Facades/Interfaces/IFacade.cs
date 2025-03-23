using CookBook.Common.Models;

namespace CookBook.Maui.Facades.Interfaces;

public interface IFacade<TListModel, TDetailModel>
    where TListModel: ModelBase
    where TDetailModel : ModelBase
{
    Task<IList<TListModel>> GetAllItemsAsync();
}
