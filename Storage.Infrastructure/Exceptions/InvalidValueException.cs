namespace Storage.Infrastructure.Exceptions;

public sealed class InvalidValueException:Exception
{
	public InvalidValueException(string message):base (message)  { }
}
