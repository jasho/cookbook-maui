namespace CookBook.Common.Models; 

public record ImageModel : ModelBase {

	public Guid? Id { get; set; }
	public byte[] Data { get; set; } = Array.Empty<byte>();
}
