namespace Storage.Core.Models;

public sealed class Book:BaseModel
{
    public string? Name { get; set; }

    #region Included property ids

    public Guid? AithorId { get; set; }
    public Guid? TopicId { get; set; }

    #endregion

    #region Included properties

    public Author? Author { get; set; }
    public Topic? Topic { get; set; }

    #endregion
}
