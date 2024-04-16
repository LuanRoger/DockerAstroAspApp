using System.Reflection;

namespace Server.Exceptions;

public class InvalidRequestException(MemberInfo requestType, string validatioMessage) : 
    Exception(string.Format(MESSAGE, requestType.Name, validatioMessage))
{
    private const string MESSAGE = "The request of type {0} is invalid. {1}";
}