using CookBook.Common.Enums;
using CookBook.Maui.Entities;
using CookBook.Maui.Extensions;
using CookBook.Maui.Services.Interfaces;
using SQLite;

namespace CookBook.Maui.Services;

public class DatabaseService : IDatabaseService
{
    public const string DatabaseName = "database.db3";
    private readonly string databasePath;

    private readonly ISecureStorageService secureStorageService;
    private Task? initializationTask;
    private readonly Lock initializationLock = new();

    public DatabaseService(
        ISecureStorageService secureStorageService)
    {
        databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName);
        this.secureStorageService = secureStorageService;
    }

    private async Task<CreateTableResult> CreateTableAsync<T>()
        where T : EntityBase, new()
    {
        var connection = new SQLiteAsyncConnection(databasePath);
        var result = await connection.CreateTableAsync<T>();
        await connection.CloseAsync();
        return result;
    }

    private async Task<int> DropTableAsync<T>()
        where T: EntityBase, new()
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

    public async Task<T?> GetByIdAsync<T>(Guid id)
        where T : EntityBase, new()
    {
        await EnsureInitializationFinishedAsync();

        var connection = new SQLiteAsyncConnection(databasePath);
        var result = await connection.Table<T>().Where(model => model.Id == id).FirstOrDefaultAsync();
        await connection.CloseAsync();
        return result;
    }

    public async Task<Guid> CreateOrUpdateAsync<T>(T entity)
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
            var existingEntity = await GetByIdAsync<T>(entity.Id);
            if(existingEntity is null)
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
        Task? initializationTask;
        lock (initializationLock)
        {
            initializationTask = this.initializationTask;
        }

        if (initializationTask is not null)
        {
            await initializationTask;
        }
    }

    private async Task SeedIngredientsAsync()
    {
        await CreateOrUpdateAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Vejce",
            Description =
                "Základní význam vajec domácí drůbeže je v první řadě biologický, tj. zajistit reprodukci daného druhu. Protože k vývoji nového jedince dochází mimo tělo matky, obsahuje vejce všechny důležité výživné složky nezbytné pro vývoj nového organismu. Zatímco vejce krůt, kachen a hus jsou produkována hlavně pro účely reprodukční, tj. slouží jako vejce násadová, slepičí vejce slouží také jako vejce konzumní a mohou být součástí lidské výživy. Vejce mají vysoký obsah plnohodnotných bílkovin (obsahují všechny aminokyseliny pro člověka nezbytné a to v poměru, který je nejpříznivější ze všech běžných potravin). Vejce dále obsahují tuky, vitamíny a minerální látky. Avšak obsahují i vysoké množství cholesterolu, takže konzumace 3 a více vajec denně prokazatelně zvyšuje riziko onemocnění a smrti.[1]",
            ImageUrl = "ingredient_egg.jpg"
        });

        await CreateOrUpdateAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Cibule",
            Description =
                "Jedná se o dvouletou až vytrvalou (spíše jen teoreticky[zdroj?!]) rostlinu, na bázi s velkou cibulí. Stonek je dosti robustní, dole až 3 cm v průměru, je dutý. Listy jsou jednoduché, přisedlé, s listovými pochvami. Čepele jsou celokrajné, polooblé se souběžnou žilnatinou. Květy jsou oboupohlavní, ve vrcholovém květenství, jedná se o hlávkovitě stažený zdánlivý okolík, ve skutečnosti to je stažené vrcholičnaté květenství zvané šroubel. Květenství je podepřeno toulcem. Pacibulky jsou v květenství přítomny jen někdy. Okvětí se skládá ze 6 okvětních lístků bílé až narůžovělé barvy, se středním zeleným pruhem. Tyčinek je 6. Gyneceum je složeno ze 3 plodolistů, je synkarpní, semeník je svrchní. Plodem je tobolka.",
            ImageUrl = "ingredient_onion.jpg"
        });

        await CreateOrUpdateAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Slanina",
            Description =
                "Slanina nebo také špek je označení pro solené či uzené vepřové sádlo. Vyrábí se převážně z vepřového bůčku, kýty nebo hřbetu. Samotná slanina se vyrábí naložením do soli na několik týdnů a případně pozdějším vyuzením.\r\n\r\nVýraz se také používá jako zkrácený název pro anglickou slaninu, která kromě sádla obsahuje i maso."
        });

        await CreateOrUpdateAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Rajče",
            Description = "Rajče jedlé, též lilek rajče (Solanum lycopersicum) je trvalka bylinného charakteru pěstovaná jako jednoletka. Patří do čeledi lilkovitých. Pochází ze Střední a Jižní Ameriky. Plodem je bobule zvaná rajče, původně rajské jablko, proto se rajče řadí mezi plodovou zeleninu, ale jsou spekulace o tom, že rajče je ovoce.",
            ImageUrl = "ingredient_tomato.jpg"
        });

        await CreateOrUpdateAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Mléko",
            Description = "Mlíko je atypický způsob čepování piva. Oblíbily si ho především ženy, díky výsledné jemnější a mírně nasládlé chuti.[zdroj?]",
            ImageUrl = "ingredient_milk.jpg"
        });
    }

    private async Task SeedRecipesAsync()
    {
        await CreateOrUpdateAsync(new RecipeEntity
        {
            Id = Guid.Empty,
            Name = "Míchaná vejce",
            Description = "Popis míchaných vajec",
            Duration = TimeSpan.FromMinutes(15),
            FoodType = FoodType.MainDish,
            ImageUrl = "recipe_scrambled_eggs.jpg"
        });

        await CreateOrUpdateAsync(new RecipeEntity
        {
            Id = Guid.Empty,
            Name = "Miso polévka",
            Description = "Popis miso polévky",
            Duration = TimeSpan.FromMinutes(30),
            FoodType = FoodType.Soup,
            ImageUrl = "recipe_miso_soup.jpg",
        });

        await CreateOrUpdateAsync(new RecipeEntity
        {
            Id = Guid.Empty,
            Name = "Vykoštěné kuře s citronem a bylinkami",
            Description = "Popis kuřete",
            Duration = TimeSpan.FromHours(1),
            FoodType = FoodType.MainDish,
            ImageUrl = "recipe_chicken_with_lemon_and_herbs.jpg",
        });

        await CreateOrUpdateAsync(new RecipeEntity
        {
            Id = Guid.Empty,
            Name = "Citronový sorbet",
            Description = "Popis dezertu",
            Duration = TimeSpan.FromHours(1),
            FoodType = FoodType.Dessert,
        });
    }
}