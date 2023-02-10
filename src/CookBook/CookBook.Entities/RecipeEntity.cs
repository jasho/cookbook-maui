using System;
using System.Collections.Generic;
using CookBook.Entities.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CookBook.Entities;

[Table(nameof(RecipeEntity))]
public record RecipeEntity : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TimeSpan Duration { get; set; }
    public FoodType FoodType { get; set; }
    public string ImageUrl { get; set; }

    [OneToMany(inverseForeignKey: nameof(IngredientAmountEntity.RecipeId), CascadeOperations = CascadeOperation.All)]
    public List<IngredientAmountEntity> IngredientAmounts { get; set; } = new();
}