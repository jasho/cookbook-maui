using CookBook.Api.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.DAL.Mappers;

[Mapper]
public partial class IngredientMapper
{
    public partial void EntityToEntity(IngredientEntity source, IngredientEntity destination);
}
