using CookBook.Common.Enums;
using CookBook.Maui.Entities;
using CookBook.Maui.Services.Interfaces;
using SQLite;

namespace CookBook.Maui.Services;

public class DatabaseService(
    ISecureStorageService secureStorageService,
    IGlobalExceptionService globalExceptionService)
    : IDatabaseService
{
    private const string DatabaseName = "database.db3";
    private readonly string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName);

    private readonly List<Guid> ingredientGuids =
    [
        new("df935095-8709-4040-a2bb-b6f97cb416dc"),
        new("23b3902d-7d4f-4213-9cf0-112348f56238"),
        new("7f251cd6-3ac4-49be-b3e7-d1f9f7cfdd3a"),
    ];

    private readonly List<Guid> recipeGuids =
    [
        new("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        new("a8ee7ce8-9903-4f42-afb4-b2c34dfb7ccf"),
        new("c3542130-589c-4302-a441-a110fcadd45a"),
    ];

    private Task? initializationTask;
    private readonly Lock initializationLock = new();

    private async Task<CreateTableResult> CreateTableAsync<T>()
        where T : EntityBase, new()
    {
        var connection = new SQLiteAsyncConnection(databasePath);
        var result = await connection.CreateTableAsync<T>();
        await connection.CloseAsync();
        return result;
    }

    private async Task<int> DropTableAsync<T>()
        where T : EntityBase, new()
    {
        var connection = new SQLiteAsyncConnection(databasePath);
        var result = await connection.DropTableAsync<T>();
        await connection.CloseAsync();
        return result;
    }

    public async Task<List<T>> GetAllAsync<T>()
        where T : new()
    {
        await EnsureInitializationFinishedAsync();

        var connection = new SQLiteAsyncConnection(databasePath);
        var result = await connection.Table<T>().ToListAsync();
        await connection.CloseAsync();
        return result ?? [];
    }

    public async Task<T?> GetByIdAsync<T>(Guid id, bool isCalledFromDatabaseInitialization = false)
        where T : EntityBase, new()
    {
        if(isCalledFromDatabaseInitialization is false)
        {
            await EnsureInitializationFinishedAsync();
        }

        var connection = new SQLiteAsyncConnection(databasePath);
        var result = await connection.Table<T>().Where(model => model.Id == id).FirstOrDefaultAsync();
        await connection.CloseAsync();
        return result;
    }

    public async Task<Guid> CreateOrUpdateAsync<T>(T entity, bool isCalledFromDatabaseInitialization = false)
        where T : EntityBase, new()
    {
        var connection = new SQLiteAsyncConnection(databasePath);
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
            await connection.InsertAsync(entity);
        }
        else
        {
            var existingEntity = await GetByIdAsync<T>(entity.Id, isCalledFromDatabaseInitialization);
            if (existingEntity is null)
            {
                await connection.InsertAsync(entity);
            }
            else
            {
                await connection.UpdateAsync(entity);
            }
        }

        await connection.CloseAsync();

        return entity.Id;
    }

    public async Task DeleteAsync<T>(Guid id)
    {
        var connection = new SQLiteAsyncConnection(databasePath);
        await connection.DeleteAsync<T>(id);
        await connection.CloseAsync();
    }

    public void InitializeDatabase()
    {
        initializationTask = Task.Run(async () =>
        {
            var isFirstRun = await secureStorageService.GetIsFirstRunAsync();
            if (isFirstRun is false)
            {
                await DropTableAsync<IngredientEntity>();
                await DropTableAsync<RecipeEntity>();
            }

            await CreateTableAsync<IngredientEntity>();
            await CreateTableAsync<RecipeEntity>();

            await SeedIngredientsAsync();
            await SeedRecipesAsync();

            await secureStorageService.SetIsFirstRunAsync(false);
        });
    }

    private async Task EnsureInitializationFinishedAsync()
    {
        Task? initializationTaskLocal;
        lock (initializationLock)
        {
            initializationTaskLocal = initializationTask;
        }

        if (initializationTaskLocal is not null)
        {
            try
            {
                await initializationTaskLocal;
            }
            catch (Exception e)
            {
                globalExceptionService.HandleException(e);
            }
        }
    }

    private async Task SeedIngredientsAsync()
    {
        await CreateOrUpdateAsync(new IngredientEntity
        {
            Id = ingredientGuids[0],
            Name = "Chicken Egg",
            Description = "Chicken eggs are widely used in many types of dish, both sweet and savory, including many baked goods. Some of the most common preparation methods include scrambled, fried, poached, hard-boiled, soft-boiled, omelettes, and pickled. They also may be eaten raw, although this is not recommended for people who may be especially susceptible to salmonellosis, such as the elderly, the infirm, or pregnant women. In addition, the protein in raw eggs is only 51 percent bioavailable, whereas that of a cooked egg is nearer 91 percent bioavailable, meaning the protein of cooked eggs is nearly twice as absorbable as the protein from raw eggs.",
            ImageUrl = "ingredient_egg.jpg"
        }, true);

        await CreateOrUpdateAsync(new IngredientEntity
        {
            Id = ingredientGuids[1],
            Name = "Yellow Onion",
            Description = "Yellow or brown onions are sweet, with many cultivars bred specifically to accentuate this sweetness, such as Vidalia, Walla Walla, Cévennes, and Bermuda. Yellow onions turn a rich, dark brown when caramelised and are used to add a sweet flavour to various dishes, such as French onion soup.",
            ImageUrl = "ingredient_onion.jpg"
        }, true);

        await CreateOrUpdateAsync(new IngredientEntity
        {
            Id = ingredientGuids[2],
            Name = "Bacon",
            Description =
                "Bacon is a type of salt-cured pork made from various cuts, typically the belly or less fatty parts of the back. It is eaten as a side dish (particularly in breakfasts), used as a central ingredient (e.g., the BLT sandwich), or as a flavouring or accent. Regular bacon consumption is associated with increased mortality and other health concerns."
        }, true);
    }

    private async Task SeedRecipesAsync()
    {
        await CreateOrUpdateAsync(new RecipeEntity
        {
            Id = recipeGuids[0],
            Name = "Scrambled eggs",
            Description = "Scrambled eggs is a dish made from eggs (usually chicken eggs), where the whites and yolks have been stirred, whipped, or beaten together (typically with salt, butter or oil, and sometimes water or milk, or other ingredients), then heated so that the proteins denature and coagulate, and they form into \"curds\"",
            Duration = TimeSpan.FromMinutes(15),
            FoodType = FoodType.MainDish,
            ImageUrl = "recipe_scrambled_eggs.jpg"
        }, true);

        await CreateOrUpdateAsync(new RecipeEntity
        {
            Id = recipeGuids[1],
            Name = "Miso soup",
            Description = "Miso soup is a traditional Japanese soup consisting of miso paste mixed with a dashi stock. It is commonly served as part of an ichijū-sansai meal, meaning \"one soup, three dishes,\" a traditional Japanese meal structure that includes rice, soup, and side dishes. Optional ingredients based on region and season may be added, such as wakame, tofu, negi, abura-age, mushrooms, etc. Along with suimono (clear soups), miso soup is considered to be one of the two basic soup types of Japanese cuisine. It is a representative of soup dishes served with rice",
            Duration = TimeSpan.FromMinutes(30),
            FoodType = FoodType.Soup,
            ImageUrl = "recipe_miso_soup.jpg",
        }, true);

        await CreateOrUpdateAsync(new RecipeEntity
        {
            Id = recipeGuids[2],
            Name = "Lemon sorbet",
            Description = "Sorbet is a frozen dessert made using ice combined with fruit juice, fruit purée, or other ingredients, such as wine, liqueur, or honey.\r\n\r\nSorbet does not contain dairy products. Sherbet is similar to sorbet, but contains dairy.",
            Duration = TimeSpan.FromHours(1),
            FoodType = FoodType.Dessert,
        }, true);
    }
}