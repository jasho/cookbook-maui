using CookBook.Common.Models;
using CookBook.Maui.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Maui.Mappers;

[Mapper]
public partial class IngredientMapper
{
    public partial List<IngredientListModel> IngredientsToIngredientListModels(IEnumerable<IngredientEntity> entities);
    public partial IngredientDetailModel? IngredientToIngredientDetailModel(IngredientEntity? entity);
}