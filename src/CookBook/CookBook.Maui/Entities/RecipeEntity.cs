using CookBook.Common.Enums;

namespace CookBook.Maui.Entities
{
    public record RecipeEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string ImageUrl { get; set; }
    }
}