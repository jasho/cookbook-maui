using AutoMapper;
using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Repositories.Interfaces;

namespace CookBook.Api.DAL.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly IStorage storage;
    private readonly IMapper mapper;

    public ImageRepository(
        IStorage storage,
        IMapper mapper)
    {
        this.storage = storage;
        this.mapper = mapper;
    }

    public IList<ImageEntity> GetAll()
    {
        return storage.Images;
    }

    public ImageEntity? GetById(Guid id)
    {
        return storage.Images.SingleOrDefault(entity => entity.Id == id);
    }

    public Guid Insert(ImageEntity image)
    {
        storage.Images.Add(image);
        return image.Id;
    }

    public Guid? Update(ImageEntity entity)
    {
        var imageExisting = storage.Images.SingleOrDefault(imageInStorage =>
            imageInStorage.Id == entity.Id);
        if (imageExisting != null)
        {
            mapper.Map(entity, imageExisting);
        }

        return imageExisting?.Id;
    }

    public void Remove(Guid id)
    {
        var imageToRemove = storage.Images.Single(image => image.Id.Equals(id));
        storage.Images.Remove(imageToRemove);
    }

    public bool Exists(Guid id)
    {
        return storage.Images.Any(image => image.Id == id);
    }
}