namespace Storage.Infrastructure.Dtos;

public abstract class BaseDto
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; }
}
