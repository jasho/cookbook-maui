using CookBook.Api.DAL.Entities;
using CookBook.Common.Enums;

namespace CookBook.Api.DAL;

public class Storage : IStorage
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

    private readonly List<Guid> ingredientAmountGuids =
    [
        new("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        new("87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
    ];

    private readonly List<Guid> recipeGuids =
    [
        new("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
        new("a8ee7ce8-9903-4f42-afb4-b2c34dfb7ccf"),
        new("c3542130-589c-4302-a441-a110fcadd45a"),
        new("2caa29d8-61f0-4c1d-850d-4d70003e6aef"),
    ];

    public List<IngredientEntity> Ingredients { get; } = [];
    public List<IngredientAmountEntity> IngredientAmounts { get; } = [];
    public List<RecipeEntity> Recipes { get; } = [];
    public List<ImageEntity> Images { get; } = [];

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
            Name = "Chicken Egg",
            Description = "Chicken eggs are widely used in many types of dish, both sweet and savory, including many baked goods. Some of the most common preparation methods include scrambled, fried, poached, hard-boiled, soft-boiled, omelettes, and pickled. They also may be eaten raw, although this is not recommended for people who may be especially susceptible to salmonellosis, such as the elderly, the infirm, or pregnant women. In addition, the protein in raw eggs is only 51 percent bioavailable, whereas that of a cooked egg is nearer 91 percent bioavailable, meaning the protein of cooked eggs is nearly twice as absorbable as the protein from raw eggs.",
            ImageUrl = "https://i.ibb.co/d7mZWGN/image.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[1],
            Name = "Yellow Onion",
            Description = "Yellow or brown onions are sweet, with many cultivars bred specifically to accentuate this sweetness, such as Vidalia, Walla Walla, Cévennes, and Bermuda. Yellow onions turn a rich, dark brown when caramelised and are used to add a sweet flavour to various dishes, such as French onion soup.",
            ImageUrl = "https://i.ibb.co/sbXC0rS/480px-Onion-on-White.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[2],
            Name = "Bacon",
            Description =
                "Bacon is a type of salt-cured pork made from various cuts, typically the belly or less fatty parts of the back. It is eaten as a side dish (particularly in breakfasts), used as a central ingredient (e.g., the BLT sandwich), or as a flavouring or accent. Regular bacon consumption is associated with increased mortality and other health concerns."
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[3],
            Name = "Tomato",
            Description = "Tomatoes, with their umami flavor, are extensively used in Mediterranean cuisine as a key ingredient in pizza and many pasta sauces. Tomatoes are used in Spanish gazpacho and Catalan pa amb tomàquet. The tomato is a crucial and ubiquitous part of Middle Eastern cuisine, served fresh in salads (e.g., Arab salad, Israeli salad, Shirazi salad and Turkish salad), grilled with kebabs and other dishes, made into sauces, and so on.",
            ImageUrl = "https://i.ibb.co/1TzsF6B/ingredient-7.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[4],
            Name = "Milk",
            Description = "Milk is a white liquid food produced by the mammary glands of lactating mammals. It is the primary source of nutrition for young mammals (including breastfed human infants) before they are able to digest solid food. Milk contains many nutrients, including calcium and protein, as well as lactose and saturated fat; the enzyme lactase is needed to break down lactose. Immune factors and immune-modulating components in milk contribute to milk immunity. The first milk, which is called colostrum, contains antibodies and immune-modulating components that strengthen the immune system against many diseases.",
            ImageUrl = "https://i.ibb.co/BB3gVxr/ingredient-2.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[5],
            Name = "Rice",
            Description = "Rice is a cereal grain and in its domesticated form is the staple food of over half of the world's population, particularly in Asia and Africa. Rice is the seed of the grass species Oryza sativa (Asian rice)—or, much less commonly, Oryza glaberrima (African rice). Asian rice was domesticated in China some 13,500 to 8,200 years ago; African rice was domesticated in Africa about 3,000 years ago. Rice has become commonplace in many cultures worldwide; in 2021, 787 million tons were produced, placing it fourth after sugarcane, maize, and wheat. Only some 8% of rice is traded internationally. China, India, and Indonesia are the largest consumers of rice. A substantial amount of the rice produced in developing nations is lost after harvest through factors such as poor transport and storage. Rice yields can be reduced by pests including insects, rodents, and birds, as well as by weeds, and by diseases such as rice blast. Traditional rice polycultures such as rice-duck farming, and modern integrated pest management seek to control damage from pests in a sustainable way.",
            ImageUrl = "https://i.ibb.co/98CGN3H/ingredient-3.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[6],
            Name = "Lemon",
            Description = "Lemon is a rich source of vitamin C, providing 64% of the Daily Value in a 100 g reference amount (table). Other essential nutrients are low in content.",
            ImageUrl = "https://i.ibb.co/0KQgsdT/ingredient-4.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[7],
            Name = "Chicken meat",
            Description = "Chicken is the most common type of poultry in the world. Owing to the relative ease and low cost of raising chickens—in comparison to mammals such as cattle or hogs—chicken meat (commonly called just \"chicken\") and chicken eggs have become prevalent in numerous cuisines.",
            ImageUrl = "https://i.ibb.co/4KVB05k/ingredient-5.jpg"
        });

        Ingredients.Add(new IngredientEntity
        {
            Id = ingredientGuids[8],
            Name = "Chilli pepper",
            Description = "Chili peppers, also spelled chile or chilli, are varieties of berry-fruit plants from the genus Capsicum, which are members of the nightshade family Solanaceae, cultivated for their pungency. They are used as a spice to add pungency (spicy heat) in many cuisines. Capsaicin and the related capsaicinoids give chili peppers their intensity when ingested or applied topically. Chili peppers exhibit a range of heat and flavors. This diversity is the reason behind the availability of different types of chili powder, each offering its own taste and heat level.",
            ImageUrl = "https://i.ibb.co/VDB2bQT/ingredient-6.jpg"
        });
    }

    private void SeedIngredientAmounts()
    {
        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[0],
            Amount = 4.0,
            Unit = Unit.Pieces,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[0]
        });

        IngredientAmounts.Add(new IngredientAmountEntity
        {
            Id = ingredientAmountGuids[1],
            Amount = 1.0,
            Unit = Unit.Pieces,
            RecipeId = recipeGuids[0],
            IngredientId = ingredientGuids[1]
        });
    }

    private void SeedRecipes()
    {
        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[0],
            Name = "Scrambled eggs",
            Description = "Scrambled eggs is a dish made from eggs (usually chicken eggs), where the whites and yolks have been stirred, whipped, or beaten together (typically with salt, butter or oil, and sometimes water or milk, or other ingredients), then heated so that the proteins denature and coagulate, and they form into \"curds\"",
            Duration = TimeSpan.FromMinutes(15),
            FoodType = FoodType.MainDish,
            ImageUrl = "https://i.ibb.co/mJgrX6B/Scrambled-eggs-01.jpg"
        });

        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[1],
            Name = "Miso soup",
            Description = "Miso soup is a traditional Japanese soup consisting of miso paste mixed with a dashi stock. It is commonly served as part of an ichijū-sansai meal, meaning \"one soup, three dishes,\" a traditional Japanese meal structure that includes rice, soup, and side dishes. Optional ingredients based on region and season may be added, such as wakame, tofu, negi, abura-age, mushrooms, etc. Along with suimono (clear soups), miso soup is considered to be one of the two basic soup types of Japanese cuisine. It is a representative of soup dishes served with rice",
            Duration = TimeSpan.FromMinutes(60),
            FoodType = FoodType.Soup,
            ImageUrl = "https://i.ibb.co/RY1XKmL/recipe-2.jpg",
        });

        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[2],
            Name = "Lemon sorbet",
            Description = "Sorbet is a frozen dessert made using ice combined with fruit juice, fruit purée, or other ingredients, such as wine, liqueur, or honey.\r\n\r\nSorbet does not contain dairy products. Sherbet is similar to sorbet, but contains dairy.",
            Duration = TimeSpan.FromMinutes(30),
            FoodType = FoodType.Dessert,
        });

        Recipes.Add(new RecipeEntity
        {
            Id = recipeGuids[3],
            Name = "Oven-roasted chicken with lemon and rosemary",
            Duration = TimeSpan.FromMinutes(60),
            FoodType = FoodType.MainDish,
            Description = "Roast chicken in the oven.",
            ImageUrl = "https://i.ibb.co/QJF2ZxX/recipe-1.jpg",
        });
    }
}