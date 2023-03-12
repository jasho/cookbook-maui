namespace CookBook.Api.DAL.Entities;

public abstract record EntityBase
{
    public Guid Id { get; init; } = Guid.NewGuid();
}