namespace Server.Exceptions;

public class ClientNotFoundException(int clientId) : 
    Exception(string.Format(MESSAGE, clientId))
{
    private const string MESSAGE = "The client with ID[{0}] was not found.";
}