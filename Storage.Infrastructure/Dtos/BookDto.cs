namespace Storage.Infrastructure.Dtos;

public sealed class BookDto:BaseDto
{
    public string? Name { get; set; }

    #region Included property ids

    public Guid? AuthorId { get; set; }
    public Guid? TopicId { get; set; }

    #endregion

    #region Included properties

    public AuthorDto? Author { get; set; }
    public TopicDto? Topic { get; set; }

    #endregion
}
