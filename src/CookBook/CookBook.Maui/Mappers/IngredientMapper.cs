using CookBook.Common.Models;
using CookBook.Maui.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Maui.Mappers;

[Mapper]
public partial class IngredientMapper
{
        public partial List<IngredientListModel> EntitiesToListModels(IEnumerable<IngredientEntity> entities);
        public partial IngredientDetailModel? EntityToDetailModel(IngredientEntity? entity);
        public partial IngredientEntity DetailModelToEntity(IngredientDetailModel detailModel);
}