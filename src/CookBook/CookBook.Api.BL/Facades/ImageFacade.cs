using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.BL.Mappers;
using CookBook.Api.DAL.Repositories.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class ImageFacade(
    IImageRepository imageRepository,
    ImageMapper imageMapper)
    : IImageFacade
{
    public ImageModel? GetById(Guid id)
    {
        var imageEntity = imageRepository.GetById(id);
        return imageEntity is null
            ? null
            : imageMapper.EntityToModel(imageEntity);
    }

    public Guid Create(ImageModel imageModel)
    {
        var imageEntity = imageMapper.ModelToEntity(imageModel);
        return imageRepository.Insert(imageEntity);
    }

    public Guid? Update(ImageModel imageModel)
    {
        var imageEntity = imageMapper.ModelToEntity(imageModel);
        return imageRepository.Update(imageEntity);
    }

    public void Delete(Guid id)
    {
        imageRepository.Remove(id);
    }
}