using CookBook.Api.DAL.Entities;

namespace CookBook.Api.DAL.Repositories.Interfaces; 

public interface IImageRepository {
	ImageEntity? GetById(Guid id);
	Guid Insert(ImageEntity image);
	Guid? Update(ImageEntity entity);
	void Remove(Guid id);
	bool Exists(Guid id);
}