using CookBook.Common.Models;
using CookBook.Maui.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Maui.Mappers;

[Mapper]
public partial class RecipeMapper
{
    public partial List<RecipeListModel> EntitiesToListModels(IEnumerable<RecipeEntity> entities);
    
    [MapperIgnoreSource(nameof(RecipeEntity.Description))]
    [MapperIgnoreSource(nameof(RecipeEntity.Duration))]
    private partial RecipeListModel EntityToListModel(RecipeEntity entity);

    [MapperIgnoreTarget(nameof(RecipeDetailModel.IngredientAmounts))]
    public partial RecipeDetailModel? EntityToDetailModel(RecipeEntity? entity);

    [MapperIgnoreSource(nameof(RecipeDetailModel.IngredientAmounts))]
    public partial RecipeEntity DetailModelToEntity(RecipeDetailModel detailModel);
}
