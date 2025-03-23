using CookBook.Api.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.DAL.Mappers;

[Mapper]
public partial class ImageMapper
{
    public partial void EntityToEntity(ImageEntity source, ImageEntity destination);
}
