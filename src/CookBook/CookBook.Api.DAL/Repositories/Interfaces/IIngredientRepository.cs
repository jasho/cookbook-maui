using CookBook.Api.Entities;

namespace CookBook.Api.Repositories
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