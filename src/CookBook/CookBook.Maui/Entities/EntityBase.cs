using System.ComponentModel.DataAnnotations;

namespace CookBook.Maui.Entities;

public record EntityBase
{
    [Key]
    public Guid Id { get; set; }
}