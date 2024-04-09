namespace Server.Controllers.Exceptions;

public class InvalidRequestException(Type requestType, string validatioMessage) : 
    Exception(string.Format(MESSAGE, requestType.Name, validatioMessage))
{
    private const string MESSAGE = "The request of type {0} is invalid. {1}";
}