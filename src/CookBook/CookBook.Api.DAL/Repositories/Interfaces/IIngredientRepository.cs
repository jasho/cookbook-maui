using CookBook.Api.DAL.Entities;

namespace CookBook.Api.DAL.Repositories.Interfaces
{
    public interface IIngredientRepository
    {
        IList<IngredientEntity> GetAll();
        IngredientEntity? GetById(Guid id);
        Guid Insert(IngredientEntity ingredient);
        Guid? Update(IngredientEntity entity);
        void Remove(Guid id);
        bool Exists(Guid id);
    }
}