using CookBook.Common.Models;

namespace CookBook.Common.Data;

public interface IDataSeedService
{
    List<IngredientDetailModel> GetIngredientSeedData();
}

public class DataSeedService : IDataSeedService
{
    private readonly List<Guid> ingredientGuids =
    [
        new("df935095-8709-4040-a2bb-b6f97cb416dc"),
        new("23b3902d-7d4f-4213-9cf0-112348f56238"),
        new("7f251cd6-3ac4-49be-b3e7-d1f9f7cfdd3a"),
        new("adb7daf1-8a6c-4cbb-b4f5-631a9b7f7287"),
        new("a8978e5d-0c5b-449c-9dc0-0a01563c0c3b"),
        new("0e88301e-cd92-47cf-8ee7-5cb0752e9f82"),
        new("e79f129f-3153-41df-8e84-8bcd7a077648"),
        new("a62a2fb6-2b80-45b1-8f82-1401a6834abe"),
        new("78c2a34b-1e84-40c8-bc59-49510478679d"),
    ];

    public List<IngredientDetailModel> GetIngredientSeedData()
        =>
        [
            new()
            {
                Id = ingredientGuids[0],
                Name = "Chicken Egg",
                Description = "Chicken eggs are widely used in many types of dish, both sweet and savory, including many baked goods. Some of the most common preparation methods include scrambled, fried, poached, hard-boiled, soft-boiled, omelettes, and pickled. They also may be eaten raw, although this is not recommended for people who may be especially susceptible to salmonellosis, such as the elderly, the infirm, or pregnant women. In addition, the protein in raw eggs is only 51 percent bioavailable, whereas that of a cooked egg is nearer 91 percent bioavailable, meaning the protein of cooked eggs is nearly twice as absorbable as the protein from raw eggs.",
                ImageUrl = "https://i.ibb.co/d7mZWGN/image.jpg"
            },
            new()
            {
                Id = ingredientGuids[1],
                Name = "Yellow Onion",
                Description = "Yellow or brown onions are sweet, with many cultivars bred specifically to accentuate this sweetness, such as Vidalia, Walla Walla, Cévennes, and Bermuda. Yellow onions turn a rich, dark brown when caramelised and are used to add a sweet flavour to various dishes, such as French onion soup.",
                ImageUrl = "https://i.ibb.co/sbXC0rS/480px-Onion-on-White.jpg"
            }
        ];
}