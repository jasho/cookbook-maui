using CookBook.Api.DAL.Entities;
using CookBook.Common.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.BL.Mappers;

[Mapper]
public partial class RecipeMapper
{
    public partial List<RecipeListModel> EntitiesToListModels(IEnumerable<RecipeEntity> entities);
    public partial RecipeDetailModel EntityToDetailModel(RecipeEntity entity);
    public partial RecipeEntity DetailModelToEntity(RecipeDetailModel detailModel);
}
