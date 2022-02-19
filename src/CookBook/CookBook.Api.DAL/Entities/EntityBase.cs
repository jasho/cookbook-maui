namespace CookBook.Api.Entities
{
    public abstract record EntityBase
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}