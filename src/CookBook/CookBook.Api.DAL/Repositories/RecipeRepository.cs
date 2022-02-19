using AutoMapper;
using CookBook.Api.Entities;

namespace CookBook.Api.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IMapper mapper;
        private readonly IStorage storage;

        public RecipeRepository(
            IMapper mapper,
            IStorage storage)
        {
            this.mapper = mapper;
            this.storage = storage;
        }

        public IList<RecipeEntity> GetAll()
        {
            return storage.Recipes;
        }

        public RecipeEntity? GetById(Guid id)
        {
            var recipeEntity = storage.Recipes.SingleOrDefault(recipe => recipe.Id == id);

            if (recipeEntity is not null)
            {
                recipeEntity.IngredientAmounts = GetIngredientAmountsByRecipeId(id);
                foreach (var ingredientAmount in recipeEntity.IngredientAmounts)
                {
                    ingredientAmount.Ingredient = storage.Ingredients.SingleOrDefault(ingredientEntity => ingredientEntity.Id == ingredientAmount.IngredientId);
                }
            }

            return recipeEntity;
        }

        public Guid Insert(RecipeEntity entity)
        {
            storage.Recipes.Add(entity);

            foreach (var ingredientAmount in entity.IngredientAmounts)
            {
                var ingredientAmountEntity = ingredientAmount with { RecipeId = entity.Id };
                storage.IngredientAmounts.Add(ingredientAmountEntity);
            }

            return entity.Id;
        }

        public Guid? Update(RecipeEntity entity)
        {
            var recipeEntityExisting = storage.Recipes.SingleOrDefault(recipeEntity => recipeEntity.Id == entity.Id);

            if (recipeEntityExisting is not null)
            {
                mapper.Map(entity, recipeEntityExisting);
                recipeEntityExisting.IngredientAmounts = GetIngredientAmountsByRecipeId(entity.Id);
                UpdateIngredientAmounts(entity, recipeEntityExisting);
                return recipeEntityExisting.Id;
            }
            else
            {
                return null;
            }
        }

        private void UpdateIngredientAmounts(RecipeEntity updatedEntity, RecipeEntity existingEntity)
        {
            var ingredientAmountsToDelete = existingEntity.IngredientAmounts.Where(t =>
                !updatedEntity.IngredientAmounts.Select(a => a.Id).Contains(t.Id));
            DeleteIngredientAmounts(ingredientAmountsToDelete);

            var recipeUpdateIngredientModelsToInsert = updatedEntity.IngredientAmounts.Where(t =>
                !existingEntity.IngredientAmounts.Select(a => a.Id).Contains(t.Id));
            InsertIngredientAmounts(existingEntity, recipeUpdateIngredientModelsToInsert);

            var recipeUpdateIngredientModelsToUpdate = updatedEntity.IngredientAmounts.Where(
                ingredient => existingEntity.IngredientAmounts.Any(ia => ia.IngredientId == ingredient.IngredientId));
            UpdateIngredientAmounts(existingEntity, recipeUpdateIngredientModelsToUpdate);
        }

        private void UpdateIngredientAmounts(RecipeEntity recipeEntity,
            IEnumerable<IngredientAmountEntity> recipeIngredientModelsToUpdate)
        {
            foreach (var recipeUpdateIngredientModel in recipeIngredientModelsToUpdate)
            {
                IngredientAmountEntity? ingredientAmountEntity;
                if (recipeUpdateIngredientModel.Id == null)
                {
                    ingredientAmountEntity = GetIngredientAmountRecipeIdAndIngredientId(recipeEntity.Id,
                        recipeUpdateIngredientModel.IngredientId);
                }
                else
                {
                    ingredientAmountEntity = storage.IngredientAmounts.Single(t => t.Id == recipeUpdateIngredientModel.Id);
                }

                if (ingredientAmountEntity is not null)
                {
                    ingredientAmountEntity.Amount = recipeUpdateIngredientModel.Amount;
                    ingredientAmountEntity.Unit = recipeUpdateIngredientModel.Unit;
                    ingredientAmountEntity.IngredientId = recipeUpdateIngredientModel.IngredientId;
                }
            }
        }

        private void DeleteIngredientAmounts(IEnumerable<IngredientAmountEntity> ingredientAmountsToDelete)
        {
            var toDelete = ingredientAmountsToDelete.ToList();
            for (var i = 0; i < toDelete.Count; i++)
            {
                var ingredientAmountEntity = toDelete.ElementAt(i);
                storage.IngredientAmounts.Remove(ingredientAmountEntity);
            }
        }

        private void InsertIngredientAmounts(RecipeEntity existingEntity,
            IEnumerable<IngredientAmountEntity> recipeIngredientAmountsToInsert)
        {
            foreach (var ingredientAmount in recipeIngredientAmountsToInsert)
            {
                var ingredientAmountEntity = ingredientAmount with { RecipeId = existingEntity.Id };
                storage.IngredientAmounts.Add(ingredientAmountEntity);
            }
        }

        private IList<IngredientAmountEntity> GetIngredientAmountsByRecipeId(Guid recipeId)
        {
            return storage.IngredientAmounts.Where(ingredientAmountEntity => ingredientAmountEntity.RecipeId == recipeId).ToList();
        }

        private IngredientAmountEntity? GetIngredientAmountRecipeIdAndIngredientId(Guid recipeId, Guid ingredientId)
        {
            return storage.IngredientAmounts.SingleOrDefault(entity => entity.RecipeId == recipeId && entity.IngredientId == ingredientId);
        }

        public void Remove(Guid id)
        {
            var ingredientAmountsToRemove = storage.IngredientAmounts.Where(ingredientAmount => ingredientAmount.RecipeId == id).ToList();

            for (var i = 0; i < ingredientAmountsToRemove.Count; i++)
            {
                var ingredientAmountToRemove = ingredientAmountsToRemove.ElementAt(i);
                storage.IngredientAmounts.Remove(ingredientAmountToRemove);
            }

            var recipeToRemove = storage.Recipes.SingleOrDefault(recipeEntity => recipeEntity.Id == id);
            if (recipeToRemove is not null)
            {
                storage.Recipes.Remove(recipeToRemove);
            }
        }

        public bool Exists(Guid id)
        {
            return storage.Recipes.Any(recipe => recipe.Id == id);
        }
    }
}