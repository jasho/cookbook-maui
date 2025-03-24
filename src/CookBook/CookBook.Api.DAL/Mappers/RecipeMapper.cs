using CookBook.Api.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.DAL.Mappers;

[Mapper]
public partial class RecipeMapper
{
    public partial void EntityToEntity(RecipeEntity source, RecipeEntity destination);
}
