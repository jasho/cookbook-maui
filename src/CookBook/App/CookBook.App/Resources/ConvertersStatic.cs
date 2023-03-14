using CookBook.App.Converters;

namespace CookBook.App.Resources
{
    public static class ConvertersStatic
    {
        public static FoodTypeToColorConverter FoodTypeToColorConverter { get; set; } = new();
        public static FoodTypeToIconConverter FoodTypeToIconConverter { get; set; } = new();
        public static FoodTypeToStringConverter FoodTypeToStringConverter { get; set; } = new();
    }
}