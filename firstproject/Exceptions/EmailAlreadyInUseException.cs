namespace firstproject.Exceptions;

public class EmailAlreadyInUseException(string message) : Exception(message)
{
}