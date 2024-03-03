namespace Server.Utils;

public static class Env
{
    private const string CONNECTION_STRING_KEY = "CONNECTION_STRING";
    
    public static string? GetConnectionString =>
        Environment.GetEnvironmentVariable(CONNECTION_STRING_KEY);
}