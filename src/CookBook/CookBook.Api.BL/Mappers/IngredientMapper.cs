using CookBook.Api.DAL.Entities;
using CookBook.Common.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.BL;

[Mapper]
public partial class IngredientMapper
{
    public partial List<IngredientListModel> EntitiesToListModels(IEnumerable<IngredientEntity> entities);
    public partial IngredientDetailModel EntityToDetailModel(IngredientEntity entity);
    public partial IngredientEntity DetailModelToEntity(IngredientDetailModel detailModel);
}
