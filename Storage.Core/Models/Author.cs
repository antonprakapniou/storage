namespace Storage.Core.Models;

public sealed class Author:BaseModel
{
    public string? Name { get; set; }
    public ICollection<Book>? Books { get; set; }
}
