namespace Storage.Infrastructure.Exceptions;

public sealed class ModelNotFoundException:Exception
{
	public ModelNotFoundException(string message):base(message) { }
}
