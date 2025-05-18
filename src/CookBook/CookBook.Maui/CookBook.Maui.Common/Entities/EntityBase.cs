using SQLite;

namespace CookBook.Maui.Entities;

public record EntityBase
{
    [PrimaryKey]
    public Guid Id { get; set; }
}