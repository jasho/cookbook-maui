using CookBook.Common.Enums;
using CookBook.Maui.Entities;
using CookBook.Maui.Services.Interfaces;
using SQLite;

namespace CookBook.Maui.Services;

public class DatabaseService : IDatabaseService
{
    public const string DatabaseName = "database.db3";

    private readonly string databasePath;

    public DatabaseService()
    {
        databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName);
    }

    private async Task<CreateTableResult> CreateTableAsync<T>()
        where T : EntityBase, new()
    {
        var connection = new SQLiteAsyncConnection(databasePath);
        var result = await connection.CreateTableAsync<T>();
        await connection.CloseAsync();
        return result;
    }

    public async Task<List<T>> GetAllAsync<T>()
        where T : new()
    {
        var connection = new SQLiteAsyncConnection(databasePath);
        var result = await connection.Table<T>().ToListAsync();
        await connection.CloseAsync();
        return result;
    }

    public async Task<T> GetByIdAsync<T>(Guid id)
        where T : EntityBase, new()
    {
        var connection = new SQLiteAsyncConnection(databasePath);
        var result = await connection.Table<T>().Where(model => model.Id == id).FirstOrDefaultAsync();
        await connection.CloseAsync();
        return result;
    }

    public async Task SetAsync<T>(T entity)
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
            await connection.UpdateAsync(entity);
        }
        await connection.CloseAsync();
    }

    public async Task CreateDatabaseAsync()
    {
        await CreateTableAsync<IngredientEntity>();
        await CreateTableAsync<RecipeEntity>();

        await SeedIngredientsAsync();
        await SeedRecipesAsync();
    }

    private async Task SeedIngredientsAsync()
    {
        await SetAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Vejce",
            Description =
                "Základní význam vajec domácí drůbeže je v první řadě biologický, tj. zajistit reprodukci daného druhu. Protože k vývoji nového jedince dochází mimo tělo matky, obsahuje vejce všechny důležité výživné složky nezbytné pro vývoj nového organismu. Zatímco vejce krůt, kachen a hus jsou produkována hlavně pro účely reprodukční, tj. slouží jako vejce násadová, slepičí vejce slouží také jako vejce konzumní a mohou být součástí lidské výživy. Vejce mají vysoký obsah plnohodnotných bílkovin (obsahují všechny aminokyseliny pro člověka nezbytné a to v poměru, který je nejpříznivější ze všech běžných potravin). Vejce dále obsahují tuky, vitamíny a minerální látky. Avšak obsahují i vysoké množství cholesterolu, takže konzumace 3 a více vajec denně prokazatelně zvyšuje riziko onemocnění a smrti.[1]",
            ImageUrl = "https://i.ibb.co/d7mZWGN/image.jpg"
        });

        await SetAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Cibule",
            Description =
                "Jedná se o dvouletou až vytrvalou (spíše jen teoreticky[zdroj?!]) rostlinu, na bázi s velkou cibulí. Stonek je dosti robustní, dole až 3 cm v průměru, je dutý. Listy jsou jednoduché, přisedlé, s listovými pochvami. Čepele jsou celokrajné, polooblé se souběžnou žilnatinou. Květy jsou oboupohlavní, ve vrcholovém květenství, jedná se o hlávkovitě stažený zdánlivý okolík, ve skutečnosti to je stažené vrcholičnaté květenství zvané šroubel. Květenství je podepřeno toulcem. Pacibulky jsou v květenství přítomny jen někdy. Okvětí se skládá ze 6 okvětních lístků bílé až narůžovělé barvy, se středním zeleným pruhem. Tyčinek je 6. Gyneceum je složeno ze 3 plodolistů, je synkarpní, semeník je svrchní. Plodem je tobolka.",
            ImageUrl = "https://i.ibb.co/sbXC0rS/480px-Onion-on-White.jpg"
        });

        await SetAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Slanina",
            Description =
                "Slanina nebo také špek je označení pro solené či uzené vepřové sádlo. Vyrábí se převážně z vepřového bůčku, kýty nebo hřbetu. Samotná slanina se vyrábí naložením do soli na několik týdnů a případně pozdějším vyuzením.\r\n\r\nVýraz se také používá jako zkrácený název pro anglickou slaninu, která kromě sádla obsahuje i maso."
        });

        await SetAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Rajče",
            Description = "Rajče jedlé, též lilek rajče (Solanum lycopersicum) je trvalka bylinného charakteru pěstovaná jako jednoletka. Patří do čeledi lilkovitých. Pochází ze Střední a Jižní Ameriky. Plodem je bobule zvaná rajče, původně rajské jablko, proto se rajče řadí mezi plodovou zeleninu, ale jsou spekulace o tom, že rajče je ovoce.",
            ImageUrl = "https://i.ibb.co/1TzsF6B/ingredient-7.jpg"
        });

        await SetAsync(new IngredientEntity
        {
            Id = Guid.Empty,
            Name = "Mléko",
            Description = "Mlíko je atypický způsob čepování piva. Oblíbily si ho především ženy, díky výsledné jemnější a mírně nasládlé chuti.[zdroj?]",
            ImageUrl = "https://i.ibb.co/BB3gVxr/ingredient-2.jpg"
        });
    }

    private async Task SeedRecipesAsync()
    {
        await SetAsync(new RecipeEntity
        {
            Id = Guid.Empty,
            Name = "Míchaná vejce",
            Description = "Popis míchaných vajec",
            Duration = TimeSpan.FromMinutes(15),
            FoodType = FoodType.MainDish,
            ImageUrl = "https://i.ibb.co/mJgrX6B/Scrambled-eggs-01.jpg"
        });

        await SetAsync(new RecipeEntity
        {
            Id = Guid.Empty,
            Name = "Miso polévka",
            Description = "Popis miso polévky",
            Duration = TimeSpan.FromMinutes(30),
            FoodType = FoodType.Soup,
            ImageUrl = "https://i.ibb.co/RY1XKmL/recipe-2.jpg",
        });

        await SetAsync(new RecipeEntity
        {
            Id = Guid.Empty,
            Name = "Vykoštěné kuře s citronem a bylinkami",
            Description = "Popis kuřete",
            Duration = TimeSpan.FromHours(1),
            FoodType = FoodType.MainDish,
            ImageUrl = "https://i.ibb.co/QJF2ZxX/recipe-1.jpg",
        });

        await SetAsync(new RecipeEntity
        {
            Id = Guid.Empty,
            Name = "Citronový sorbet",
            Description = "Popis dezertu",
            Duration = TimeSpan.FromHours(1),
            FoodType = FoodType.Dessert,
        });
    }
}