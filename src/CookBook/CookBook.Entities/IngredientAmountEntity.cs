using System;
using CookBook.Entities.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CookBook.Entities;

[Table(nameof(IngredientAmountEntity))]
public record IngredientAmountEntity : EntityBase
{
    public double Amount { get; set; }
    public Unit Unit { get; set; }

    [ForeignKey(typeof(RecipeEntity))]
    public Guid? RecipeId { get; set; }
    [ManyToOne(nameof(RecipeId))]
    public RecipeEntity? Recipe { get; set; }

    [ForeignKey(typeof(IngredientEntity))]
    public Guid? IngredientId { get; set; }
    [OneToOne]
	public IngredientEntity? Ingredient { get; set; }
}