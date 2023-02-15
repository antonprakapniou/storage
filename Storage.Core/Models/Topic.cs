namespace Storage.Core.Models;

public sealed class Topic:BaseModel
{
    public string? Name { get; set; }
    public ICollection<Book>? Books { get; set; }
}
