using AutoMapper;
using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Repositories.Interfaces;

namespace CookBook.Api.DAL.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly IStorage storage;
        private readonly IMapper mapper;

        public IngredientRepository(
            IStorage storage,
            IMapper mapper)
        {
            this.storage = storage;
            this.mapper = mapper;
        }

        public IList<IngredientEntity> GetAll()
        {
            return storage.Ingredients;
        }

        public IngredientEntity? GetById(Guid id)
        {
            return storage.Ingredients.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(IngredientEntity ingredient)
        {
            storage.Ingredients.Add(ingredient);
            return ingredient.Id;
        }

        public Guid? Update(IngredientEntity entity)
        {
            var ingredientExisting = storage.Ingredients.SingleOrDefault(ingredientInStorage =>
                ingredientInStorage.Id == entity.Id);
            if (ingredientExisting != null)
            {
                mapper.Map(entity, ingredientExisting);
            }

            return ingredientExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var ingredientAmountsToRemove =
                storage.IngredientAmounts.Where(ingredientAmount => ingredientAmount.IngredientId == id).ToList();

            for (var i = 0; i < ingredientAmountsToRemove.Count; i++)
            {
                var ingredientAmountToRemove = ingredientAmountsToRemove.ElementAt(i);
                storage.IngredientAmounts.Remove(ingredientAmountToRemove);
            }

            var ingredientToRemove = storage.Ingredients.Single(ingredient => ingredient.Id.Equals(id));
            storage.Ingredients.Remove(ingredientToRemove);
        }

        public bool Exists(Guid id)
        {
            return storage.Ingredients.Any(ingredient => ingredient.Id == id);
        }
    }
}