using CookBook.Api.DAL.Entities;
using CookBook.Common.Enums;

namespace CookBook.Api.DAL;

public class Storage : IStorage
{
    private readonly IList<Guid> ingredientGuids = new List<Guid>
    {
        new ("df935095-8709-4040-a2bb-b6f97cb416dc"),
        new ("23b3902d-7d4f-4213-9cf0-112348f56238"),
        new ("7f251cd6-3ac4-49be-b3e7-d1f9f7cfdd3a"),
        new ("adb7daf1-8a6c-4cbb-b4f5-631a9b7f7287"),
        new ("a8978e5d-0c5b-449c-9dc0-0a01563c0c3b"),
        new ("0e88301e-cd92-47cf-8ee7-5cb0752e9f82"),
        new ("e79f129f-3153-41df-8e84-8bcd7a077648"),
        new ("a62a2fb6-2b80-45b1-8f82-1401a6834abe"),
        new ("78c2a34b-1e84-40c8-bc59-49510478679d"),
        new ("5590cc1b-f3b7-443f-808e-a354ac3c3d93"),
        new ("91c01c8f-6469-42e5-9a7d-f4841bcd89f0"),
        new ("aab20a1a-093c-4736-ac61-d4d908702336"),
        new ("73d2cde6-efe1-4ab1-8886-be8ae14e7089"),
        new ("8ffa759e-0aea-4e3d-aa6e-eddbd79b5cf2"),
        new ("95121bc8-25a0-4542-9793-666f99312ea3"),
    };

    private readonly IList<Guid> ingredientAmountGuids = new List<Guid>
    {
        new ("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        new ("87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
        new ("d6bd4e68-bcc9-4ffe-8c96-32e0662dc27f"),
        new ("3360f954-aa75-4e30-bf0d-834cc43df1bf"),
        new ("05c93139-fd08-4ae7-9d7f-c32fb0c0dc53"),
        new ("a5745ee9-d93a-4a51-b5a9-dc56818e8376"),
        new ("015bac7c-dfe4-4b2c-80f7-7d16e2c72111"),
        new ("273f5d1d-05f8-4c54-8d72-44602fa8c848"),
        new ("03351f17-4beb-4eeb-8f92-e24e101f5f1e"),
        new ("a8860b05-7204-40f4-90a5-01430b4dfcad"),
        new ("75eb0dc4-4e80-46b7-837d-b9420ed8b6aa"),
        new ("0ed2b565-fdfb-45cf-9985-fe58f6d69e8a"),
        new ("ab75e906-cda5-4494-86d0-93400e6aa7d8"),
        new ("6fbcb34b-4640-4e21-abc3-3cf29a5474c8"),
    };

    private readonly IList<Guid> recipeGuids = new List<Guid>
    {
        new ("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        new ("a8ee7ce8-9903-4f42-afb4-b2c34dfb7ccf"),
        new ("c3542130-589c-4302-a441-a110fcadd45a"),
        new ("2caa29d8-61f0-4c1d-850d-4d70003e6aef"),
    };

    public IList<IngredientEntity> Ingredients { get; } = new List<IngredientEntity>();
    public IList<IngredientAmountEntity> IngredientAmounts { get; } = new List<IngredientAmountEntity>();
    public IList<RecipeEntity> Recipes { get; } = new List<RecipeEntity>();

    public Storage(bool seedData = true)
    {
        if (seedData)
        {
            SeedIngredients();
            SeedIngredientAmounts();
            SeedRecipes();
        }
    }

    private void SeedIngredients()
    {
        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[0],
            Name = "Butter",
            Description = "Butter description",
            ImageUrl = "https://i.ibb.co/VT9ZhyD/ingredient-butter.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[1],
            Name = "Onion",
            Description = "Onion description",
            ImageUrl = "https://i.ibb.co/SmKJS9m/ingredient-onion.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[2],
            Name = "Carrot",
            Description = "Carrot description",
            ImageUrl = "https://i.ibb.co/p01mW3T/ingredient-carrot.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[3],
            Name = "Celery",
            Description = "Celery description",
            ImageUrl = "https://i.ibb.co/b26MxnG/ingredient-celery.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[4],
            Name = "Garlic",
            Description = "Garlic description",
            ImageUrl = "https://i.ibb.co/kBcvWLj/ingredient-garlic.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[5],
            Name = "Flour",
            Description = "Flour description",
            ImageUrl = "https://i.ibb.co/gD2gKvK/ingredient-flour.png"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[6],
            Name = "Chicken broth",
            Description = "Chicken broth description",
            ImageUrl = "https://i.ibb.co/718yxwT/ingredient-chicken-broth.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[7],
            Name = "Heavy cream",
            Description = "Heavy cream description",
            ImageUrl = "https://i.ibb.co/S37rbgd/ingredient-heavy-cream.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[8],
            Name = "Muenster cheese",
            Description = "Muenster cheese description",
            ImageUrl = "https://i.ibb.co/zNv5jkB/ingredient-muenster-cheese.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[9],
            Name = "White wine",
            Description = "White wine description",
            ImageUrl = "https://i.ibb.co/n13vbmQ/ingredient-white-wine.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[10],
            Name = "Pale beer",
            Description = "Pale beer description",
            ImageUrl = "https://i.ibb.co/xXj1ZSZ/ingredient-pale-beer.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[11],
            Name = "Worcestershire sauce",
            Description = "Worcestershire sauce description",
            ImageUrl = "https://i.ibb.co/gD54vDp/ingredient-worcestershire-sauce.png"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[12],
            Name = "Hot sauce",
            Description = "Hot sauce description",
            ImageUrl = "https://i.ibb.co/FnC60WX/ingredient-hot-sauce.jpg"
        });
    }

    private void SeedIngredientAmounts()
    {
        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[0],
            Amount = 4.0,
            Unit = Unit.Spoons,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[0]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[1],
            Amount = 1.0,
            Unit = Unit.Cups,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[1]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[2],
            Amount = 0.5,
            Unit = Unit.Cups,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[2]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[3],
            Amount = 0.5,
            Unit = Unit.Cups,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[3]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[4],
            Amount = 1.0,
            Unit = Unit.Spoons,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[4]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[5],
            Amount = 0.25,
            Unit = Unit.Cups,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[5]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[6],
            Amount = 1.0,
            Unit = Unit.L,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[6]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[7],
            Amount = 1.0,
            Unit = Unit.Cups,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[7]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[8],
            Amount = 300.0,
            Unit = Unit.Ml,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[8]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[9],
            Amount = 1.0,
            Unit = Unit.Cups,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[9]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[10],
            Amount = 1.0,
            Unit = Unit.Cups,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[10]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[11],
            Amount = 1.0,
            Unit = Unit.Spoons,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[11]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[12],
            Amount = 0.5,
            Unit = Unit.TeaSpoons,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[12]
        });

    }

    private void SeedRecipes()
    {
        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[0],
            Name = "Broccoli beer cheese soup",
            Description = """
            Start with butter, onions, carrots, celery, garlic until cooked down
            Add flour, stir well, cook for 4-5 mins
            Add chicken broth, bring to a boil
            Add wine and reduce to a simmer
            Add cream, cheese, Worcestershire, and hot sauce
            Serve with croutons
            """,
            Duration = TimeSpan.FromMinutes(15),
            FoodType = FoodType.Soup,
            ImageUrl = "https://i.ibb.co/6Dy5CQm/recipe-broccoli-beer-cheese-soup.jpg",
            IngredientAmounts = new List<IngredientAmountEntity>
            {
                IngredientAmounts[0],
                IngredientAmounts[1],
                IngredientAmounts[2],
                IngredientAmounts[3],
                IngredientAmounts[4],
                IngredientAmounts[5],
                IngredientAmounts[6],
                IngredientAmounts[7],
                IngredientAmounts[8],
                IngredientAmounts[9],
                IngredientAmounts[10],
                IngredientAmounts[11],
                IngredientAmounts[12],
            }
        });
            
        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[1],
            Name = "Miso soup",
            Duration = TimeSpan.FromMinutes(60),
            FoodType = FoodType.Soup,
            Description = "Soup!",
            ImageUrl = "https://i.ibb.co/RY1XKmL/recipe-2.jpg",
        });


        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[2],
            Name = "Boneless chicken with lemon and herbs",
            Duration = TimeSpan.FromMinutes(60),
            FoodType = FoodType.MainDish,
            Description = "Description of chicken",
            ImageUrl = "https://i.ibb.co/QJF2ZxX/recipe-1.jpg",
        });

        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[3],
            Name = "Lemon sorbet",
            Duration = TimeSpan.FromMinutes(30),
            FoodType = FoodType.Dessert,
            Description = "Dessert",
        });
    }
}