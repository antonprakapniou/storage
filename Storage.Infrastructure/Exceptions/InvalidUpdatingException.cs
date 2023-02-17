namespace Storage.Infrastructure.Exceptions;

public sealed class InvalidUpdatingException:Exception
{
	public InvalidUpdatingException(string message) : base(message) { }
}
