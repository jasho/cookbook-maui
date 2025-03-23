using CookBook.Api.DAL.Entities;
using CookBook.Common.Models;
using Riok.Mapperly.Abstractions;

namespace CookBook.Api.BL.Mappers;

[Mapper]
public partial class ImageMapper
{
    public partial List<ImageModel> EntitiesToModels(IEnumerable<ImageEntity> entities);
    public partial ImageModel EntityToModel(ImageEntity entity);
    public partial ImageEntity ModelToEntity(ImageModel model);
}
