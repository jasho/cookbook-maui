using CookBook.Common.Models;
using CookBook.Maui.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Maui.Mappers;

[Mapper]
public partial class RecipeMapper
{
    public partial List<RecipeListModel> EntitiesToListModels(IEnumerable<RecipeEntity> entities);
    public partial RecipeDetailModel? EntityToDetailModel(RecipeEntity? entity);
    public partial RecipeEntity DetailModelToEntity(RecipeDetailModel detailModel);
}
