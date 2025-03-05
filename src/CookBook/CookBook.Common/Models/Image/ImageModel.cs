namespace CookBook.Common.Models;

public class ImageModel : ModelBase
{
    public Guid? Id { get; set; }
    public byte[] Data { get; set; } = [];
}