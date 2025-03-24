using CookBook.Api.DAL.Entities;
using CookBook.Common.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.BL.Mappers;

[Mapper]
public partial class IngredientAmountMapper
{
    public partial List<IngredientAmountEntity> ModelsToEntities(IEnumerable<IngredientAmountModel> entities);
}
