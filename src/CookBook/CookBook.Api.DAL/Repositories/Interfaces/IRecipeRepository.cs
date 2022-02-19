using CookBook.Api.Entities;

namespace CookBook.Api.Repositories
{
    public interface IRecipeRepository
    {
        IList<RecipeEntity> GetAll();
        RecipeEntity? GetById(Guid id);
        Guid Insert(RecipeEntity entity);
        Guid? Update(RecipeEntity entity);
        void Remove(Guid id);
        bool Exists(Guid id);
    }
}