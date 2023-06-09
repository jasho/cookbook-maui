using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades.Interfaces;

public interface IImageFacade
{
    ImageModel? GetById(Guid id);
    Guid Create(ImageModel imageModel);
    Guid? Update(ImageModel imageModel);
    void Delete(Guid id);
}