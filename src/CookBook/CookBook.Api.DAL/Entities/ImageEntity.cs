namespace CookBook.Api.DAL.Entities;

public record ImageEntity : EntityBase
{
    public Guid Id { get; set; }

    public byte[] Image { get; set; } = Array.Empty<byte>();

    public string ImageB64
    {
        get => Convert.ToBase64String(Image);
        set => Image = Convert.FromBase64String(value);
    }
}