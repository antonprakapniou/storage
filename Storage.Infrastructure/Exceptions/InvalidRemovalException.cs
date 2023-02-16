namespace Storage.Infrastructure.Exceptions;

public sealed class InvalidRemovalException:Exception
{
	public InvalidRemovalException(string message) : base(message) { }
}
